# https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-python
# ^ Credit given to the site above for this code: "Quickstart: Use Python to query a database"
# This code showcases the steps to take to make a simple connection to Azure SQL Database.

# WARNING: The credentials shown in this file are private to the POWResearch group.
# It is wise to remove the credentials from the file if the project repo ever goes public
import pyodbc

server = 'power-rocket-server.database.windows.net'
database = 'POWR_Rocket_database'
username = 'powrrocket2022'
password = '{uncgcapstone2022!}'
driver = '{ODBC Driver 17 for SQL Server}' # Will not work unless ODBC Driver 17 for SQL Server is installed on the user's environment.

with pyodbc.connect(
        'DRIVER=' + driver + ';SERVER=tcp:' + server + ';PORT=1433;DATABASE=' + database + ';UID=' + username + ';PWD=' + password) as conn:
    with conn.cursor() as cursor:
      # Enter the SQL command in the execute() parameter in quotes
        cursor.execute("create table stop_making_new_tables")
