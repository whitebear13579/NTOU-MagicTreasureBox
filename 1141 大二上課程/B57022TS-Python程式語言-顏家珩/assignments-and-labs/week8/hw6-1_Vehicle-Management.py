'''

hw 6-1 Vehicle Management
請設計一個能管理多種類交通工具的系統 系統需要能夠建立 汽車 (Car)、機車 (Motorcycle)、與 腳踏車 (Bicycle) 三種物件
並將它們統一儲存在一個清單中 每種交通工具都繼承自一個父類別 Vehicle，並各自有自己的屬性與輸出格式
請用物件導向
輸入說明：
Car 品牌 年份 座位數
Motorcycle 品牌 年份 排氣量
Bicycle 品牌 年份 型號
print：輸出所有交通工具的資訊
stop：結束程式
#input
Car Honda 2019 4
Motorcycle Yamaha 2022 150
Bicycle Giant 2021 Road
print
stop
#output
Car: Honda, Year: 2019, Seat: 4
Motorcycle: Yamaha, Year: 2022, cc: 150
Bicycle: Giant, Year: 2021, Model: Road

'''


class vehicle:
    def __init__ (self, name, year):
        self._brand = name
        self._year = year

    def output( self ):
        return

class Car(vehicle):
    def __init__ (self, name, year, num):
        super().__init__(name, year)
        self._seatsCount = num
    def output(self):
        print(f"Car: {self._brand}, Year: {self._year}, Seat: {self._seatsCount}")

class Motorcycle(vehicle):
    def __init__ (self, name, year, num):
        super().__init__(name, year)
        self._exhaust = num
    def output(self):
        print(f"Motorcycle: {self._brand}, Year: {self._year}, cc: {self._exhaust}")

class Bicycle(vehicle):
    def __init__ (self, name, year, num):
        super().__init__(name, year)
        self._model = num
    def output(self):
        print(f"Bicycle: {self._brand}, Year: {self._year}, Model: {self._model}")

miku = list()

while True:
    getLine = str(input())
    cmd = getLine.split()

    if not cmd:
        continue

    oper = cmd[0]
    if oper == "stop":
        break
    elif oper == "print":
        for i in miku:
            i.output()
    else:
        in_band = cmd[1]
        in_year = cmd[2]
        if oper == "Car":
            in_type = cmd[3]
            tp = Car(in_band, in_year, in_type)
            miku.append(tp)
        elif oper == "Motorcycle":
            in_type = cmd[3]
            tp = Motorcycle(in_band, in_year, in_type)
            miku.append(tp)
        elif oper == "Bicycle":
            in_type = " ".join(cmd[3:])
            tp = Bicycle(in_band, in_year, in_type)
            miku.append(tp)