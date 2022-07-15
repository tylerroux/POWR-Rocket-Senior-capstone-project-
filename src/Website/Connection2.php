<!DOCTYPE html>
<html>
    <?php
        header("Content-Type: text/csv;charset=utf-8");

        //Needed information for the connection
        $serverName = "powrrocketserver.database.windows.net";
        $connectionOptions = array(
            "Database" => "POWR-Rocket_DB",
            "Uid" => "powrrocket2022",
            "PWD" => "uncgcapstone2022!"
        );
        //Establishes the connection
        $conn = sqlsrv_connect($serverName, $connectionOptions);

        //The code shown in this php  below contains portions of code from this site: 
         //http://www.freekb.net/Article?id=848 This helps create the csv file from azure and makes headers 
         //to reference the data too.
     

        $filename3 = 'testycsv.csv';
        $select3 = "SELECT id, baro_pressure FROM rocket_fields";
        $select_query3 = sqlsrv_query($conn, $select3);
        $fp = fopen('php://output', 'w');

        //Generates the csv
        fputcsv($fp, array('ID', 'Bar'));

        while($row = sqlsrv_fetch_array($select_query3,SQLSRV_FETCH_ASSOC)){
            fputcsv($fp,array_values($row));
        }

        //This allows the data to only be repeated once.
        die;

        fclose($fp);
        ?>



        
</html>