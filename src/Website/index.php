
<!DOCTYPE html>
<html>
<style>

/* Our default values set as CSS variables */
:root {
    --color-bg: #69F7BE;
    --color-text-main: #000000;
    --color-primary: #FFFF00;
    --wrapper-height: 87vh;
    --image-max-width: 300px;
    --image-margin: 3rem;
    --font-family: "HK Grotesk";
    --font-family-header: "HK Grotesk";
}

/* Import fonts */
@font-face {
    font-family: HK Grotesk;
    src: url("https://cdn.glitch.me/605e2a51-d45f-4d87-a285-9410ad350515%2FHKGrotesk-Regular.otf?v=1603136326027") format("opentype");
}

@font-face {
    font-family: HK Grotesk;
    font-weight: bold;
    src: url("https://cdn.glitch.me/605e2a51-d45f-4d87-a285-9410ad350515%2FHKGrotesk-Bold.otf?v=1603136323437") format("opentype");
}

/* Our remix on glitch button */
.btn--remix {
    font-family: HK Grotesk;
    padding: 0.75rem 1rem;
    font-size: 1.1rem;
    line-height: 1rem;
    font-weight: 500;
    height: 2.75rem;
    align-items: center;
    cursor: pointer;
    background: #FFFFFF;
    border: 1px solid #000000;
    box-sizing: border-box;
    border-radius: 4px;
    text-decoration: none;
    color: #000;
    white-space: nowrap;
    margin-left: auto;
}

.btn--remix img {
    margin-right: 0.5rem;
}

.btn--remix:hover {
    background-color: #D0FFF1;
}

body{
font-family: HK Grotesk;
background-color: navy; /* For browsers that do not support gradients */
background-image: linear-gradient(gray, lightgray, gray);
}

/* Page structure */
.wrapper {
    min-height: var(--wrapper-height);
    display: grid;
    place-items: center;
    margin: 0 1rem;
}

.content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

/* Very light scaling for our illustration */
.title {
    color: black;
    font-family: HK Grotesk;
    font-style: normal;
    font-weight: bold;
    font-size: 100px;
    line-height: 105%;
    margin: 0;
}

/* Very light scaling for our illustration */
.illustration {
    width: 500px;
    height: 300px;
    margin-top: var(--image-margin);
}

/* Instructions */
.instructions {
    margin: 1rem auto 0;
}

button,
input {
    font-family: inherit;
    font-size: 100%;
    background: #FFFFFF;
    border: 1px solid #000000;
    box-sizing: border-box;
    border-radius: 4px;
    padding: 0.5rem 1rem;
    transition: 500ms;
}

h2 {
    color: black;
}

#options {
    display: grid;
    grid-template-rows: 50px 50px;
    grid-template-columns: 100px 100px 100px;
}


ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}

li {
  display: inline;
}

#links {
    color: white;
    font-size: 20px;
}
#frame {
    margin-left: 400px;
}
</style>
<head>
    <!--To include needed CSS libraries-->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">

    <title>Weather Sensor Rocket!</title>
</head>
<body>

    <!-- The wrapper and content divs set margins and positioning -->
    <div class="wrapper">
      <div class="content" role="main">
        <!-- This is the start of content for our page -->
        <h1 class="title">POWR Rocket!</h1>
        <ul>
          <!--Links to the other parts of the website-->  
          <li><a id="links" href="List.php">Data Tables</a></li>&nbsp&nbsp&nbsp&nbsp<li><a id="links" href="Graph.php">Graphs</a></li>
        </ul>
        <h2>Pressure Observation Weather Research Rocket</h2>

        <h4>Click the picture below to go to Thomas Knepshields Storm Chasing Site</h4>
        
        <!--Image that contains the link to Thomas Knepshield's website-->
        <a href="https://www.tkchasing.com/storm-chasing" target="_blank"
        ><img
        src="https://cdn.glitch.global/e177775a-5521-4bb5-9598-9c738bf4bbcd/tornadoPic.jpg?v=1643128532837"
        class="illustration"
        alt="From tkchasing.com"
        title="Click the image!"
        /></a>
 
        <!-- Instructions on using this project -->
        <div class="instructions">
          <h2>Explaining Our Experiment!</h2>
          <!--Description of the experiment-->
          <p>
            This experiment will include engineering a device to detect
            different weather patterns, record different types of data, send
            them to a database and then display the data on this webpage.<br />
          </p>
          <!--Code below is for the youtube video-->
          <iframe id="frame" width="560" height="315" src="https://www.youtube.com/embed/KCE6phEds-o" title="YouTube video player" frameborder="0" 
            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
        </div>
      </div>
    </div>

    <!--Establish Connection to database and generate the csv-->
    <?php
        header("Content-Type: text/csv;charset=utf-8");
        //Set server name and connection components
        $serverName = "powrrocketserver.database.windows.net";
        $connectionOptions = array(
            "Database" => "POWR-Rocket_DB",//Database name
            "Uid" => "powrrocket2022",//Username
            "PWD" => "uncgcapstone2022!"//Password
        );
        //Establishes the connection
        $conn = sqlsrv_connect($serverName, $connectionOptions);

        //The code shown in this php  below contains portions of code from this site: 
         //http://www.freekb.net/Article?id=848 This helps create the csv file from azure and makes headers 
         //to reference the data too.
        $filename = 'testcsv.csv';
        $select = "SELECT * FROM rocket_fields";
        $select_query = sqlsrv_query($conn, $select);
        $fp = fopen($filename, 'w');

        //Generate the csv file
        fputcsv($fp, array('ID', 'Lat', 'Long', 'Alt', 'Bar', 'Temp', 'Hum'));
        
        //Loop to add all of the values to the csv
        while($row = sqlsrv_fetch_array($select_query,SQLSRV_FETCH_ASSOC)){
            fputcsv($fp,array_values($row));
        }

        //This allows the data to only be repeated once.
        die;

        fclose($fp);
       ?>

</html>