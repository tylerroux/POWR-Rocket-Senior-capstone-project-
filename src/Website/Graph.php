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

    /* Basic page style resets */
    * {
        box-sizing: border-box;
    }

    /* Heading Font.*/
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

    /* Background color */
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

    /* The title of the page font.*/
    .title {
        color: black;
        font-family: HK Grotesk;
        font-style: normal;
        font-weight: bold;
        font-size: 100px;
        line-height: 105%;
        margin: 0;
    }

    ul {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }

    li {
        display: inline;
    }

    #myDiv{
        width: 1000px;
        height: 400px;
        margin: 0 auto;
    }

    #myDiv2{
        width: 1000px;
        height: 400px;
        margin: 0 auto;
    }

    #myDiv3{
        width: 1000px;
        height: 400px;
        margin: 0 auto;
    }

    #myDiv4{
        width: 1000px;
        height: 400px;
        margin: 0 auto;
    }

    .hidden {
        display: none;
    }

    #every {
        display: none;
    }

    #first {
        display:inline-block;margin-right:10px;
        color: white;
    }
    #second {
        display:inline-block;
        color: black;
    }

    #navbar {
        overflow: hidden;
        background-color: #333;
        position: fixed;
        top: 0; /* Position the navbar at the top of the page */
        width: 100%; /* Full width */
        z-index: 9999;
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
    <div id="navbar" class="content navbar" role="main">
            <!-- This is the start of content for our page -->
            <ul>
                <!--Links to the other parts of the website-->
                <li id="first"><a href="index.php" style="color: white">Home</a></li>
                <li id="second"><a href="List.php" style="color: white">Data tables</a></li>
                <!--Taken from https://www.w3schools.com/howto/howto_js_scroll_to_top.asp-->
                <button id="second" onclick="topFunction()">Back To The Top</button>
            </ul>
        </div>
        <h1 class="title">POWR Graphs!</h1>
    </div>

    <div class="container-fluid space">
       <!--Buttons to display the graphs-->
      <button id="showAll" onclick="displayGraph('showAll')">Show All</button>  
      <button id="toggle" onclick="displayGraph('toggle')">Show/Hide Coordinates</button> 
      <button id="toggle2" onclick="displayGraph('toggle2')">Show/Hide Temperature</button>
      <button id="toggle3" onclick="displayGraph('toggle3')">Show/Hide Humidity</button>
      <button id="toggle4" onclick="displayGraph('toggle4')">Show/Hide BaroPressure</button>
      <button id="hideAll" onclick="displayGraph('hideAll')">Hide All</button>
    </div>

    <!--From https://sebhastian.com/javascript-show-hide-div-onclick-toggle/#:~:text=To%20display%20or%20hide%20a,
    which%20is%20block%20)%20to%20none%20.-->
    <!--These div tags are place holders to house the graphs-->
    <div id="every">
    <div id="myDiv" class="hidden"></div>
    <br/>
    <div id="myDiv2" class="hidden"></div>
    <br/>
    <div id="myDiv3" class="hidden"></div>
    <br/>
    <div id="myDiv4" class="hidden"></div>
    </div>

    <!--Code for back to the top button-->
    <script>
        //Taken from https://www.w3schools.com/howto/howto_js_scroll_to_top.asp
        function topFunction() {
            document.body.scrollTop = 0; // For Safari
            document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        }
    </script>

    <!--Taken from https://sebhastian.com/javascript-show-hide-div-onclick-toggle/#:~:text=To%20display%20or%20hide%20a,
    which%20is%20block%20)%20to%20none%20.-->

    <!--This code helps establish the backend for the graph control buttons-->
    <script>
    function displayGraph(choice) {
    const targetDiv = document.getElementById("myDiv");
    const targetDiv2 = document.getElementById("myDiv2");
    const targetDiv3 = document.getElementById("myDiv3");
    const targetDiv4 = document.getElementById("myDiv4");
    const targetDiv5 = document.getElementById("every");

    var divDisplay = getComputedStyle(targetDiv).display;
    var divDisplay2 = getComputedStyle(targetDiv2).display;
    var divDisplay3 = getComputedStyle(targetDiv3).display;
    var divDisplay4 = getComputedStyle(targetDiv4).display;
    var divDisplay5 = getComputedStyle(targetDiv5).display;
    
    switch(choice) {
     case 'toggle': 
        targetDiv5.style.display = "block"; 
        if (divDisplay == "none") {
            targetDiv.style.display = "block";
        } else {
            targetDiv.style.display = "none";
        }
        break;
     case 'toggle2':
        targetDiv5.style.display = "block";
        if (divDisplay2 == "none") {
            targetDiv2.style.display = "block";
        } else {
            targetDiv2.style.display = "none";
        }
        break;
     case 'toggle3':   
        targetDiv5.style.display = "block";
        if (divDisplay3 == "none") {
            targetDiv3.style.display = "block";
        } else {
            targetDiv3.style.display = "none";
        }
        break;
     case 'toggle4':
        targetDiv5.style.display = "block";
        if (divDisplay4 == "none") {
            targetDiv4.style.display = "block";
        } else {
            targetDiv4.style.display = "none";
        }
        break;
     case 'hideAll':
        targetDiv5.style.display = "none";
        if (targetDiv.style.display == "none" && targetDiv2.style.display == "none" && 
            targetDiv.style.display3 == "none" && targetDiv4.style.display == "none") {
            targetDiv.style.display = "block";
            targetDiv2.style.display = "block";
            targetDiv3.style.display = "block";
            targetDiv4.style.display = "block";
        } else {
            targetDiv.style.display = "none";
            targetDiv2.style.display = "none";
            targetDiv3.style.display = "none";
            targetDiv4.style.display = "none";
        }
        break;
     case 'showAll':
        targetDiv5.style.display = "block";
        targetDiv.style.display = "block";
        targetDiv2.style.display = "block";
        targetDiv3.style.display = "block";
        targetDiv4.style.display = "block";
        break;
    }
 }
  </script>
</body>


<head>
    <!--Needed imports-->
    <script src="https://cdn.plot.ly/plotly-latest.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.17/d3.min.js"></script>
</head>

<body>
    <!--The code shown in this script element below contains portions of code from this site: 
    https://plotly.com/javascript/3d-line-plots/-->

    <!--The code segments below are what generate the different graphs-->
    <script>
        //Generates the 3d graph for Coordinates
        d3.csv(
            "https://powr-rocket.azurewebsites.net/testcsv.csv",
            function(err, rows) {
                function unpack(rows, key) {
                    return rows.map(function(row) {
                        return row[key];
                    });
                }

                var pointCount = 200;

                var x = unpack(rows, "Long");
                var y = unpack(rows, "Lat");
                var z = unpack(rows, "Alt");

                var layout ={
                    title: "Coordinates",
                    scene:{
                        xaxis:{title: 'Longitude'},
                        yaxis:{title: 'Latitude'},
                        zaxis:{title: 'Altitude (meters)'},
                    },
                };

                Plotly.newPlot("myDiv", [{
                    type: "scatter3d",
                    mode: "lines+markers",
                    x: x,
                    y: y,
                    z: z,
                    line: {
                        width: 1,
                    },
                    marker: {
                        size: 3.5,
                        cmin: -20,
                        cmax: 50,
                    },
                },], layout);
            }
        );
    </script>
</body>

<body>
    <!--The code shown in this script element below contains portions of code from this site: 
    https://plotly.com/javascript/3d-line-plots/-->
    <script>
        //Generates the graph for Temperature
        d3.csv(
            "https://powr-rocket.azurewebsites.net/testcsv.csv",
            function(err, rows) {
                function unpack(rows, key) {
                    return rows.map(function(row) {
                        return row[key];
                    });
                }

                var x = unpack(rows, "ID");
                var y = unpack(rows, "Temp");

                var layout2 ={
                    title: "Temperature",
                    xaxis:{title: 'ID'},
                    yaxis:{title: 'Temperature (°C)'},
                };

                Plotly.newPlot("myDiv2", [{
                    type: "scatter",
                    mode: "lines",
                    x: x,
                    y: y,
                    fill: 'tonexty',
                    line: {
                        width: .5,
                    },
                },
            ], layout2);
        });
    </script>
</body>

<body>
    <!--The code shown in this script element below contains portions of code from this site: 
    https://plotly.com/javascript/3d-line-plots/-->
    <script>
        //Generates the graph for Humidity
        d3.csv(
            "https://powr-rocket.azurewebsites.net/testcsv.csv",
            function(err, rows) {
                function unpack(rows, key) {
                    return rows.map(function(row) {
                        return row[key];
                    });
                }

                var x = unpack(rows, "ID");
                var y = unpack(rows, "Hum");

                var layout3 ={
                    title: "Humidity",
                    xaxis:{title: 'ID'},
                    yaxis:{title: 'Humidity (x100%)'},
                };

                Plotly.newPlot("myDiv3", [{
                    type: "scatter",
                    mode: "lines",
                    x: x,
                    y: y,
                    fill: 'tonexty',
                    line: {
                        width: .5,
                    },

                },
            ], layout3);
        });
    </script>
</body>

<body>
    <!--The code shown in this script element below contains portions of code from this site: 
    https://plotly.com/javascript/3d-line-plots/-->
    <script>
        //Generates the graph for Baro_Pressure
        d3.csv(
            "https://powr-rocket.azurewebsites.net/testcsv.csv",
            function(err, rows) {
                function unpack(rows, key) {
                    return rows.map(function(row) {
                        return row[key];
                    });
                }

                var x = unpack(rows, "ID");
                var y = unpack(rows, "Bar");

                var layout4 ={
                    title: "Baro_pressure",
                    xaxis:{title: 'ID'},
                    yaxis:{title: 'Baro_pressure (kPa)'},
                };

                Plotly.newPlot("myDiv4", [{
                    type: "scatter",
                    mode: "lines",
                    x: x,
                    y: y,
                    line: {
                        width: 1,
                    },
                },
            ], layout4);
        });
    </script>
</body>
</html>