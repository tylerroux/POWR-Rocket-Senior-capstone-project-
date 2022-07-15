using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports; // https://www.youtube.com/watch?v=vHeG3Gt6STE 
using System.Data.SqlClient; // https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-dotnet-visual-studio
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace POWR_Upload_Portal_2
{
    public partial class portalFrame : Form
    {
        private string[] ports; // Global string array for getting all port names
        private int arduinoConnected = 0; // Shows the status of the arduino connection; 1=TRUE
        private int timeLimitReached = 0; // Used to quit on unresponsive COM selection, 1=TRUE
        SerialPort port;
        private string[] ogdbvars = new string[5]; // holds the values for database variables to check for change
        private string ogserialvar = "";
        private string last_file_uploaded_to_storage = "";

        public portalFrame()
        {

            InitializeComponent();
            disableControls();
            refreshComs();
            enableControls();

        }

        private void refreshComs()
        {
            getAvailableComs();

            // remove all elements from the combo box
            comboBox1.Items.Clear();

            // add all elements of ports to the combo box
            for (int i = 0; i < ports.Length; i++)
            {
                comboBox1.Items.Add(ports[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        /* Gets the names of the user's available ports to be added to the COMs combobox */
        private void getAvailableComs()
        {
            ports = SerialPort.GetPortNames();
        }

        /* Disables all buttons and entry boxes from being used */
        private void disableControls()
        {
            serConnButton.Enabled = false;
            comboBox1.Enabled = false;
            dbUploadButton.Enabled = false;
            storageUploadButton.Enabled = false;
            serverNameEntry.Enabled = false;
            dbNameEntry.Enabled = false;
            usernameEntry.Enabled = false;
            passwordEntry.Enabled = false;
            dbConnButton.Enabled = false;
        }

        /* Enables all buttons and entry boxes */
        private void enableControls()
        {
            serConnButton.Enabled = true;
            comboBox1.Enabled = true;
            dbUploadButton.Enabled = true;
            storageUploadButton.Enabled = true;
            serverNameEntry.Enabled = true;
            dbNameEntry.Enabled = true;
            usernameEntry.Enabled = true;
            passwordEntry.Enabled = true;
            dbConnButton.Enabled = true;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (arduinoConnected == 0)
            {

                connectArduino();

            }
            else
            {
                disconnectArduino();
            }
        }

        private void connectArduino()
        {
            arduinoConnected = 1;
            atLabel.Text = "";

            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            ogserialvar = selectedPort; // set the global

            port = new SerialPort(selectedPort, 9600, Parity.None);
            port.WriteTimeout = 6000;

            backgroundWorker1.WorkerSupportsCancellation = true;
            while (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            backgroundWorker1.RunWorkerAsync(); // begin the timer
            try
            {
                port.Open();
                while (!port.IsOpen) { }
                Thread.Sleep(2250); // Arduino takes roughly 2 seconds at most before stops trying to find SD card
                port.Write("ID\n"); // get the ID from the arduino

                // wait for the bytes to be ready to be read
                while (port.BytesToRead < 8)
                {

                    if (timeLimitReached == 1)
                    {
                        MessageBox.Show("COM connection time limit reached.\nPort name = " + port.PortName);
                        // timer is already stopped, no need to run CancelAsync()
                        arduinoConnected = 0;
                        serConnStatusLabel.Text = "Not connected";
                        dbUploadButton.Visible = false;
                        storageUploadButton.Visible = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nPort name = " + port.PortName);
                backgroundWorker1.CancelAsync();// stop the timer
                arduinoConnected = 0;
                serConnStatusLabel.Text = "Not connected";
                dbUploadButton.Visible = false;
                storageUploadButton.Visible = false;
                return;
            }
            backgroundWorker1.CancelAsync();// stop the timer

            char[] chars = new char[8];
            try
            {
                port.Read(chars, 0, 8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nPort name = " + port.PortName);
                backgroundWorker1.CancelAsync();// stop the timer
                arduinoConnected = 0;
                serConnStatusLabel.Text = "Not connected";
                dbUploadButton.Visible = false;
                storageUploadButton.Visible = false;
                return;
            }
            backgroundWorker1.CancelAsync(); // stop the timer because connected successfully
            String str = new string(chars);
            atLabel.Text += "ID: " + str;
            serConnButton.Text = "Disconnect";
            serConnStatusLabel.Text = "Connected";
            dbUploadButton.Visible = true;
            storageUploadButton.Visible = true;
            enableControls();
        }

        private void disconnectArduino()
        {
            arduinoConnected = 0;
            try
            {
                port.Close();
                serConnButton.Text = "Connect";
                atLabel.Text = "ID: ";
                enableControls();
                dbUploadButton.Visible = false;
                storageUploadButton.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                arduinoConnected = 1;
                dbUploadButton.Visible = true;
                storageUploadButton.Visible = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            disableControls();
            refreshComs();
            enableControls();
        }

        private void dbConnButton_Click(object sender, EventArgs e)
        {
            /* The following try-catch was borrowed from the following
             * website that illustrates making a basic Azure Database
             * query from C# 
             https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-dotnet-visual-studio
             */
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = serverNameEntry.Text;
                builder.UserID = usernameEntry.Text;
                builder.Password = passwordEntry.Text;
                builder.InitialCatalog = dbNameEntry.Text;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT TOP 100 * FROM " + tableNameEntry.Text;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                connectionStatusLabel.Text = "Connected";
                                saveDBVariables();
                                dbUploadButton.Visible = true;
                            }
                            while (reader.Read())
                            {
                                String str = reader.GetSqlInt32(0).ToString();
                                MessageBox.Show(str);

                            }
                        }
                    }
                }
            }
            catch (Exception o)
            {
                MessageBox.Show(o.ToString());
                dbUploadButton.Visible = false;
                connectionStatusLabel.Text = "Not connected";
            }
            Console.ReadLine();

        }

        /**
         * Saves the DB variables from the last successful connection.
         * Sets their status to check if changed before allowing user 
         * to press dbUploadButton.
         */
        private void saveDBVariables()
        {
            ogdbvars[0] = serverNameEntry.Text;
            ogdbvars[1] = dbNameEntry.Text;
            ogdbvars[2] = usernameEntry.Text;
            ogdbvars[3] = passwordEntry.Text;
            ogdbvars[4] = tableNameEntry.Text;
        }

        /**
         * Only allowed to click this if the database has made a successful connection.
         *      - Save the database variables to globals
         *      - Make sure they are unchanged
         * We will send datalog.txt to a new demo database.
         *      - If COM connected, read datalog.txt from there
         *      - else read from file on computer
         *      - else say you can't find it
         */
        private void dbUploadButton_Click(object sender, EventArgs e)
        {
            // db variables have already been saved before being able to click
            // must check if they are unchanged, or if changed throw a warning.
            if (dbUploadButton.Visible)
            {
                // if the entries still match the verified entry strings
                if (ogdbvars[0] == serverNameEntry.Text && ogdbvars[1] == dbNameEntry.Text
                    && ogdbvars[2] == usernameEntry.Text && ogdbvars[3] == passwordEntry.Text
                    && ogdbvars[4] == tableNameEntry.Text)
                {
                    // If last_file_uploaded_to_storage is not "" then use those contents for upload to DB
                    if (last_file_uploaded_to_storage != "")
                    {
                        // Read the data from the csv into byte array
                        byte[] readArray = File.ReadAllBytes(last_file_uploaded_to_storage);
                        // send to database_upload(dataBytes)
                        int check = database_upload(readArray);
                        // check return
                    }
                    // Else If COM is still available, use those contents for DB upload
                    else if (serConnStatusLabel.Text == "Connected")
                    {
                        try
                        {
                            byte[] dataBytes = gatherData();
                            if (dataBytes == null)
                            {
                                //"No upload"
                                uploadStatusLabel.Text = "No upload";
                                return;
                            }

                            int check = database_upload(dataBytes);
                            if (check < 0)
                            {
                                //"No upload"
                                uploadStatusLabel.Text = "No upload";
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            uploadStatusLabel.Text = "No upload";
                            return;
                        }
                    }
                    // Else return and say that upload was unsuccessful
                    else
                    {
                        MessageBox.Show("Database upload: Can't find source");
                        // "No upload"
                        uploadStatusLabel.Text = "No upload";
                        return;
                    }
                    // Success!
                    uploadStatusLabel.Text = "Uploaded to Database";
                }
                else // entry variables have been changed since last successful connection
                {
                    // Throw a warning ^^
                    MessageBox.Show("Not connected to database or entries have been changed since last successful connection.");
                    uploadStatusLabel.Text = "No upload";
                }
            }
            else // if button is not visible for some reason
            {
                // Die evil hacker!
                MessageBox.Show("Die evil hacker!");
                uploadStatusLabel.Text = "No upload";
            }
        }

        private void storageUploadButton_Click(object sender, EventArgs e)
        {
            // Check if visible
            if (storageUploadButton.Visible)
            {
                // Check if arduinoConnected == 1 and if combo box item has been changed
                if (arduinoConnected == 1 && comboBox1.GetItemText(comboBox1.SelectedItem) == ogserialvar)
                {
                    
                    byte[] dataBytes = gatherData();
                    if(dataBytes == null) { return; } // check if null was returned, error

                    int check = storage_upload(dataBytes);
                    if(check < 0) { return; } // check if -1 was returned, error

                    // Success!
                    uploadStatusLabel.Text = "Uploaded to Storage";
                    // Update last upload to storage
                }
                else
                {
                    MessageBox.Show("COM is not connected or COM selection in combo box is not the same.");
                    uploadStatusLabel.Text = "No upload";
                }
            }
            else
            {
                MessageBox.Show("Die evil hacker!");
            }
        }

        /* Collects csv info and uploads to database based on certain conditions:
         * csv data comes from
         * - the last file uploaded to storage, or
         * - the available COM
         * If successful return 0 else returns -1
         */
        private int database_upload(byte[] dataArray)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = serverNameEntry.Text;
                builder.UserID = usernameEntry.Text;
                builder.Password = passwordEntry.Text;
                builder.InitialCatalog = dbNameEntry.Text;

                // Clear all entries that are currently in the table
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "DELETE FROM " + tableNameEntry.Text + ";";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteReader();
                        connection.Close();
                    }
                }

                // Perform Bulk Upload
                DataTable datatable = GetDataTabletFromCSVFile(last_file_uploaded_to_storage);
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    // objbulk connection from https://www.c-sharpcorner.com/UploadFile/0c1bb2/inserting-csv-file-records-into-sql-server-database-using-as/
                    SqlBulkCopy objbulk = new SqlBulkCopy(connection);
                    objbulk.DestinationTableName = tableNameEntry.Text;

                    connection.Open();
                    objbulk.WriteToServer(datatable);
                    connection.Close();

                }

                // set last successful upload datetime
                var dt = DateTime.Now;
                dbUploadLabel.Text = "Last database upload: " + dt.ToString("h:mm tt");

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        /* Returns a Datatable for use in uploading to database
         * Credit: https://stackoverflow.com/questions/20759302/upload-csv-file-to-sql-server
         */
        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = false;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return csvData;
        }
        private int storage_upload(byte[] dataArray)
        {
            try
            {
                // add current datetime to file name, code borrowed from tutorialsteacher
                // https://www.tutorialsteacher.com/articles/convert-date-to-string-in-csharp#:~:text=Convert%20DateTime%20to%20String%20using,pattern%20defined%20by%20the%20DateTimeFormatInfo.
                var dt = DateTime.Now;
                String fileString = "datalog" + dt.ToString("_HH_mm_ss") + ".csv";

                //write csv to directory, borrowed code from Microsoft C# Docs
                String pathString = System.IO.Path.Combine(directoryEntry.Text, fileString);
                System.IO.FileStream fs = System.IO.File.Create(pathString);

                /* Add the column names before writing from dataArray by convert a C# string to a byte array
                 * found at https://www.c-sharpcorner.com/article/c-sharp-string-to-byte-array/
                 */
                // ** Rename these fields to the fields being used in your datatable **
                byte[] bytes = Encoding.ASCII.GetBytes("number1, number2, number3\n");
                foreach (byte b in bytes)
                {
                    Console.WriteLine(b);
                }
                fs.Write(bytes, 0, bytes.Length);
                // write the data bytes to the open file
                for (byte i = 0; i < dataArray.Length; i++)
                {
                    fs.WriteByte(dataArray[i]);
                }
                fs.Close();

                // remember last file uploaded
                last_file_uploaded_to_storage = pathString;
                storageUploadLabel.Text = "Last storage upload: " + dt.ToString("h:mm tt");
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        /* Communicates with Arduino to gather csv data */
        private byte[] gatherData()
        {
            // check that directory exists
            if (!Directory.Exists(directoryEntry.Text))
            {
                MessageBox.Show("Directory does not exist. Please enter a valid directory");
                return null;
            }

            port.Write("DA\n");
            Thread.Sleep(200); // allow for all bytes to be delivered

            int numbytes = port.BytesToRead;
            byte[] dataArray = new byte[numbytes];
            port.Read(dataArray, 0, numbytes);
            return dataArray;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            timeLimitReached = 0;
            for (int i = 0; i < 4; i++) // 10 seconds total, designed to listen for CancelAsync() between sleeps
            {
                Thread.Sleep(2500);
                if (backgroundWorker1.CancellationPending) // if true, break and release backgroundworker
                {
                    backgroundWorker1.Dispose();
                    // MessageBox.Show("Debug: BackgroundWorker quit");
                    break;
                }
            }
            if (!backgroundWorker1.CancellationPending)
            {
                // MessageBox.Show("Debug: Backgroundworker finished DoWork");
                timeLimitReached = 1;
            }
        }

        /*
         * Can be removed, a little bit scared to do it myself.
         */
        private void directoryEntry_TextChanged(object sender, EventArgs e)
        {

        }
    }
}