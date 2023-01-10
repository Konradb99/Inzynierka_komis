import json
import glob
import random

brands = ["Audi", "BMW", "Ford", "Peugeot", "Opel"]
models_audi = ["A3", "A4", "A5", "E-Tron", "Q5"]
models_bmw = ["Seria 3", "Seria 5", "Seria 8", "X3", "X5"]
models_ford = ["Focus", "Mondeo", "Kuga", "Mustang", "S-Max"]
models_opel = ["Astra", "Corsa", "Zafira", "Mokka", "Insignia"]
models_peugeot = ["208", "308", "508", "3008", "5008"]

def combineJson():
    data = []
    for brand in brands:
        match brand:
            case 'Audi':
                models = models_audi
            case 'BMW':
                models = models_bmw
            case 'Ford':
                models = models_ford
            case 'Opel':
                models = models_opel
            case 'Peugeot':
                models = models_peugeot
        for model in models:
            for file in glob.glob(f"GenerateData/{brand}/{model}/*.json"):
                with open(file, "rb") as infile:
                    for item in json.load(infile):
                        data.append(item)
    return data

def shuffleData(data):
    random.shuffle(data)
    return data


combined = combineJson()
result_data = shuffleData(combined)

with open("Data/Result_file.json", 'w', encoding='utf-8') as json_file:
        json.dump(result_data, json_file, ensure_ascii=False)