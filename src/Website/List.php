<!DOCTYPE html>
<html>
<style>

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

    h2 {
        color: black;
    }

    table,th, td {
        background: white;
        margin: 0 auto;
        border: 2px solid black;
        padding: 10px;
    }

    tr, th, td{
        padding: 15px;
        text-align: center;
    }

    h1 {
            text-align: center;
            color: #006600;
            font-size: xx-large;
            font-family: 'Gill Sans', 'Gill Sans MT', 
            ' Calibri', 'Trebuchet MS', 'sans-serif';
        }    
/*https://www.w3schools.com/css/css_navbar_horizontal.asp
https://www.w3schools.com/howto/howto_css_fixed_menu.asp*/ 
#navbar {
  overflow: hidden;
  background-color: #333;
  position: fixed;
  top: 0; /* Position the navbar at the top of the page */
  width: 100%; /* Full width */
}

/*From https://stackoverflow.com/questions/9067892/how-to-align-two-elements-on-the-same-line-without-changing-html*/
#first {
  display:inline-block;margin-right:10px;
  color: white;
} 
/*From https://stackoverflow.com/questions/9067892/how-to-align-two-elements-on-the-same-line-without-changing-html*/
#second {
  display:inline-block;
  color: black;
} 

/* To separate the nav bar from the rest of the content */
.main {
  margin-top: 30px; /* Add a top margin to avoid content overlay */
}

</style>

<head>
    <!--To include needed CSS libraries-->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">

    <title>Weather Sensor Rocket!</title>

    <!--The following four script tags contain the links of needed libraries such as 
    paparse and jquery!-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="papaparse.min.js"></script>
    <script src="//jquerycsvtotable.googlecode.com/files/jquery.csvToTable.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

</head>

<!--Body of the Webpage-->
<body class="container-fluid">

    <!-- The wrapper and content divs set margins and positioning -->
    <div class="wrapper container-fluid">
        <div id="navbar" class="content navbar" role="main">
            <!-- This is the start of content for our page -->
            <ul>
                <!--Links to the other parts of the websites-->
                <li id="first"><a href="index.php" style="color: white">Home</a></li>
                <li id="second"><a href="Graph.php" style="color: white">Graphs</a></li>
                <!--Taken from https://www.w3schools.com/howto/howto_js_scroll_to_top.asp-->
                <button id="second" onclick="topFunction()">Back To The Top</button>
            </ul>
        </div>
        <h1 class="title">POWR Data Tables!</h1>
    </div>


<!-- Control buttons -->
<div class="container-fluid">    
<button id="all" onclick="filterSelection('all')">All Data</button>
<button id="coordinates" onclick="filterSelection('coordinates')">Coordinates</button>
<button id="temperature" onclick="filterSelection('temperature')">Temperature</button>
<button id="pressure" onclick="filterSelection('pressure')">Pressure</button>
<button id="humidity" onclick="filterSelection('humidity')">Humidity</button>
<!--Taken from https://www.w3schools.com/howto/howto_js_scroll_to_top.asp-->
</div>

    <!--Code for back to the top button-->
    <script>
        //Taken from https://www.w3schools.com/howto/howto_js_scroll_to_top.asp
        function topFunction() {
            document.body.scrollTop = 0; // For Safari
            document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        }
    </script>

    <!--Contains the format of the tables and the source code to display them-->
    <div class="container">
        <div class="row">
            <div class="col-sm-7">
                <div class="table-responsive">  
                                            <script>  
                                            //Section of code is inspired from 
                                            //https://code.tutsplus.com/tutorials/parsing-a-csv-file-with-javascript--cms-25626  
                                            //Takes the csv file from the database and displays it.

                                            function filterSelection(choice) {
                                            $(".show").empty();
                                            switch (choice) {
                                            //Generates the table to show all of the data
                                            case 'all':
                                            $.ajax({
                                                url: "https://powr-rocket.azurewebsites.net/testcsv.csv",
                                                dataType: 'text',
                                            }).done(successfulFunction);
                                            break;
                                            //Generates the table to show the pressure data
                                            case 'pressure':
                                            $.ajax({
                                                url: "https://powr-rocket.azurewebsites.net/Connection2.php",
                                                dataType: 'text',
                                            }).done(successFunction);
                                            break;
                                            //Generates the table to show the temperate data
                                            case 'temperature':
                                            $.ajax({
                                                url: "https://powr-rocket.azurewebsites.net/Connection3.php",
                                                dataType: 'text',
                                            }).done(successFunction);
                                            break;
                                            //Generates the table to show the humidity data
                                            case 'humidity':
                                            $.ajax({
                                                url: "https://powr-rocket.azurewebsites.net/Connection4.php",
                                                dataType: 'text',
                                            }).done(successFunction);
                                            break;
                                            //Generates the table to show the coordinates data
                                            case 'coordinates':
                                            $.ajax({
                                                url: "https://powr-rocket.azurewebsites.net/Connection5.php",
                                                dataType: 'text',
                                            }).done(successFunction);
                                            break;
                                         }
                                        }
                                          
                                          function successFunction(data) {
                                              var allRows = data.split(/\r??\n|\r/);
                                              var table = '<table>';
                                              for (var singleRow = 2; singleRow < (allRows.length-1); singleRow++) {
                                                var rowCells = allRows[singleRow].split(',');
                                                for (var rowCell = 0; rowCell < rowCells.length; rowCell++) {
                                                  if (singleRow === 0) {
                                                    table += '<th>';
                                                    table += rowCells[rowCell];
                                                    table += '</th>';
                                                  } else {
                                                    table += '<td>';
                                                    table += rowCells[rowCell];
                                                    table += '</td>';
                                                  }
                                                }
                                                if (singleRow === 0) {
                                                  table += '</tr>';
                                                  table += '</thead>';
                                                  table += '<tbody>';
                                                } else {
                                                   table += '</tr>';
                                                  }
                                              } 
                                              table += '</tbody>';
                                              table += '</table>';
                                             $('.show').append(table);
                                              }


                                              function successfulFunction(data) {
                                              var allRows = data.split(/\r??\n|\r/);
                                              var table = '<table>';
                                              for (var singleRow = 0; singleRow < (allRows.length-1); singleRow++) {
                                                var rowCells = allRows[singleRow].split(',');
                                                for (var rowCell = 0; rowCell < rowCells.length; rowCell++) {
                                                  if (singleRow === 0) {
                                                    table += '<th>';
                                                    table += rowCells[rowCell];
                                                    table += '</th>';
                                                  } else {
                                                    table += '<td>';
                                                    table += rowCells[rowCell];
                                                    table += '</td>';
                                                  }
                                                }
                                                if (singleRow === 0) {
                                                  table += '</tr>';
                                                  table += '</thead>';
                                                  table += '<tbody>';
                                                } else {
                                                   table += '</tr>';
                                                  }
                                              } 
                                              table += '</tbody>';
                                              table += '</table>';
                                             $('.show').append(table);
                                              }
                                             
                                        </script>
                </div>
            </div>
        </div>
    </div>
    <!-- Div tag that houses the tables that the user can choose. -->
    <div class="show"></div>

</body>

</html>