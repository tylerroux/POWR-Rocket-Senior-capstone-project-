# Elements of this code were borrowed from Thomas Knepshield and Dymond Deans who used the code on a prior UNCG Computer Science course assignment.
# The purpose of this program is to generate random, weather-related data that is formatted to a csv file. 
#Needed Imports
import csv
from faker import Faker
from random import randint
import time
#Set up Faker Generator
fake = Faker()
#Specify number of tuples
num_records = 200
#Generate the CSV file
def generate_csv():
    with open('testData.csv', 'w', newline='') as csv_file:
        fields = ['id', 'latitude', 'longitude', 'altitude', 'pressure', 'temperature', 'humidity']
        written = csv.DictWriter(csv_file, fieldnames=fields)
        written.writeheader()
        idval = 0
        for i in range(num_records - 1):
            idval += 1
            written.writerow(
                {
                  'id' : idval,
                  'latitude' : fake.pyfloat(left_digits=2, right_digits=5, min_value=-90, max_value=90),
                  'longitude' : fake.pyfloat(left_digits=3, right_digits=5, min_value=-180, max_value=180),
                  'altitude' : fake.pyint(max_value=52800, min_value=0), #measured in feet
                  'pressure' : fake.pyfloat(left_digits=2, right_digits=2, min_value=10, max_value=50), #measured in in. Mercury
                  'temperature' : fake.pyfloat(left_digits=2, right_digits=2, min_value=32, max_value=100), #measured in Fahrenheit
                  'humidity' : fake.pyfloat(left_digits=1, right_digits=3, min_value=0, max_value=1)
                }
            )
#Main method to call the function
if __name__ == '__main__':
    i = 1
    while(i):
        generate_csv()
        print('csv file updated')
        time.sleep(5)
