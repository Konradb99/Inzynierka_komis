import json
import random

# Class of one car
class Car(object):
    Brand = ''
    Model = ''
    BodyType = ''
    DriveType = ''
    GearboxType = ''
    FuelType=''
    Price=''
    Distance=''
    ProductionYear=''
    Capacity=''
    def __init__(self, fuelType, price, distance, year, capacity, brand, model, bodyType, driveType, gearboxType):
        self.Brand=brand
        self.Model=model
        self.BodyType=bodyType
        self.DriveType=driveType
        self.GearboxType=gearboxType
        self.FuelType = fuelType
        self.Price = price
        self.Distance = distance
        self.ProductionYear = year
        self.Capacity = capacity

class StartParams(object):
    url = ''
    body = ''
    brand = ''
    model = ''
    drive = ''
    gearbox = ''
    driveName = ''
    fuel = ''
    fileName = ''
    maxPage = 0
    maxCount = 0

modelAudi = ["A3", "A4", "A5", "E-Tron", "Q5"]
modelBmw = ["Seria 3", "Seria 5", "Seria 8", "X3", "X5"]
modelFord = ["Focus", "Kuga", "Mondeo", "Mustang", "S-Max"]
modelOpel = ["Astra", "Corsa", "Insignia", "Mokka", "Zafira"]
modelPeugeot = ["208", "308", "508", "3008", "5008"]

# Prepare categorical data about cars to scrap
def startMenu():
    startParams = StartParams()
    #Input params
    print("Enter body type: ")
    startParams.body = input()
    print("Enter brand:")
    startParams.brand = input()
    print("Enter model:")
    startParams.model = input()
    print("Enter drive type (Na wszystkie koła/Na przednie koła/Na tylnie koła):")
    startParams.drive = input()
    print("Enter gearbox type (Automatyczna / Manualna )")
    startParams.gearbox = input()
    print("Enter fuel type")
    startParams.fuel = input()
    print("Enter cars to generate count")
    startParams.maxCount = input()
    #Prepare filename
    startParams.fileName = f"WebScrapper\{startParams.brand}\{startParams.model}\{startParams.brand}-{startParams.model}-{startParams.body}-{startParams.gearbox}-{startParams.driveName}.json"
    return startParams

def generateCars(startParams: StartParams):
    generatedCars = []
    for i in range(0, int(startParams.maxCount)):
        newCar = Car(
            price = random.randint(0, 1000000),
            distance = random.randint(0, 1000000),
            year = random.randint(1990, 2022),
            capacity = random.randint(0, 7000),
            brand = startParams.brand,
            model = startParams.model,
            bodyType = startParams.body,
            driveType = startParams.drive,
            fuelType = startParams.fuel,
            gearboxType = startParams.gearbox
            )
        generatedCars.append(newCar.__dict__)
    return generatedCars

def convertToJson(dataToConvert):
    print(f'Records counter: {len(dataToConvert)}')
    with open(startParams.fileName, 'w', encoding='utf-8') as json_file:
        json.dump(dataToConvert, json_file, ensure_ascii=False)


startParams = startMenu()
cars = generateCars(startParams)
convertToJson(cars)