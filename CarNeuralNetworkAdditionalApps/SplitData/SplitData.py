import json

data = []
train = []
test = []

with open("Data/Result_file.json", "rb") as infile:
    for item in json.load(infile):
        data.append(item)


def splitIntoTrainAndTest(train_size, data):
    train_items = int(len(data)*train_size)
    train = []
    test = []
    train = data[:int(train_items)]
    test = data[int(train_items):]
    return train, test

train, test = splitIntoTrainAndTest(0.8, data)

with open("Data/train.json", 'w', encoding='utf-8') as json_file:
        json.dump(train, json_file, ensure_ascii=False)
with open("Data/test.json", 'w', encoding='utf-8') as json_file:
        json.dump(test, json_file, ensure_ascii=False)