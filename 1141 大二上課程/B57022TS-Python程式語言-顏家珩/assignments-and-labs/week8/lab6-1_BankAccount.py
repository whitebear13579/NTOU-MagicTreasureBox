'''

lab 6-1 BankAccount
請設計一個模擬銀行帳戶的系統， 每個帳戶都有客戶名稱與初始餘額，並且可以進行以下操作:
create 客戶名 金額: 建立新帳戶
deposit 客戶名 金額: 存款
withdraw 客戶名 金額: 提款
balance 客戶名: 查詢帳戶餘額
stop: 結束程式
若帳戶不存在，請輸出: “Account not found”
若提款金額超出餘額，請輸出: “Insufficient funds”
請用物件導向
#input
create tom 500
create amy 1000
deposit tom 300
withdraw amy 200
balance tom
balance amy
stop
#output
800
800

'''

class bank:
    def __init__(self):
        self.accounts = dict()
    
    def create( self, name, money ):
        self.accounts[name] = money
    
    def deposit( self, name, money ):
        if name not in self.accounts:
            print("Account not found")
        else:
            self.accounts[name] += money

    def withdraw( self, name, money ):
        if name not in self.accounts:
            print("Account not found")
        elif self.accounts[name] - money < 0 :
            print("Insufficient funds")
        else:
            self.accounts[name] -= money
    
    def balance( self, name ):
        if name not in self.accounts:
            print("Account not found")
        else:
            print(self.accounts[name])

mygo = bank()

while True:
    getLine = str(input())
    cmd = getLine.split()
    if not cmd:
        continue

    oper = cmd[0]
    if oper == "stop":
        break
    
    name = cmd[1]
    money = 0
    if ( len(cmd) == 3 ):
        money = int(cmd[2])
    
    if oper == "create":
        mygo.create(name, money)
    elif oper == "deposit":
        mygo.deposit(name, money)
    elif oper == "withdraw":
        mygo.withdraw(name, money)
    elif oper == "balance":
        mygo.balance(name)
