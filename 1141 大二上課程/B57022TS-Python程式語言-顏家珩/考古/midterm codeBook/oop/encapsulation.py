class BankAccount:
    def __init__(self, account_number, balance):
        self.account_number = account_number  # public
        self._balance = balance               # protected
        self.__pin = "1234"                   # private

    def get_balance(self):        # public
        return self._balance

    def __verify_pin(self, pin):  # private
        return pin == self.__pin
