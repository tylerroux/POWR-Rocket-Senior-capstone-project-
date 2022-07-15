<!DOCTYPE html>
<html>
    <?php
        header("Content-Type: text/csv;charset=utf-8");

        //Needed information for the connection
        $serverName = "powrrocketserver.database.windows.net"; // update me
        $connectionOptions = array(
            "Database" => "POWR-Rocket_DB", // update me
            "Uid" => "powrrocket2022", // update me
            "PWD" => "uncgcapstone2022!" // update me
        );
        //Establishes the connection
        $conn = sqlsrv_connect($serverName, $connectionOptions);

        //The code shown in this php  below contains portions of code from this site: 
         //http://www.freekb.net/Article?id=848 This helps create the csv file from azure and makes headers 
         //to reference the data too.
        $filename = 'test5csv.csv';
        $select = "SELECT id, latitude, longitude, altitude FROM rocket_fields";
        $select_query = sqlsrv_query($conn, $select);
        $fp = fopen('php://output', 'w');

        //Generates the csv
        fputcsv($fp, array('ID', 'Lat', 'Long', 'Alt'));

        while($row = sqlsrv_fetch_array($select_query,SQLSRV_FETCH_ASSOC)){
            fputcsv($fp,array_values($row));
        }

        //This allows the data to only be repeated once.
        die;

        fclose($fp);
       ?>



        
</html>