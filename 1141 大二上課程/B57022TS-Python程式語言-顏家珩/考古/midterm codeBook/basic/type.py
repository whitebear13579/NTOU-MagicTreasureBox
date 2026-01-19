# type() 型別檢查
uwu = 69
print(type(uwu)) # <class 'int'>
print(type(uwu) == int) # True

# isinstance() 型別檢查
owo = 87
print(isinstance(owo, int)) # True
print(isinstance(owo, str)) # False
a = list()
print(isinstance(a, tuple)) # False
print(isinstance(a, list)) # True

# 檢查 tuple (或一組資料) 內的所有值
## isinstance 可以檢查多重型別
b = (777 ,"miku", 3.14)
print(all(isinstance(x, (int, str, float)) for x in b)) 
# True

# 檢查物件繼承關係
## isinstance 可以檢查繼承關係或是物件
class Animal:
    pass

class Dog(Animal):  # Dog 繼承自 Animal
    pass
print(type(Dog()) == Dog) # True
print(type(Dog()) == Animal) # False
print(isinstance(Dog(), Dog)) # True
print(isinstance(Dog(), Animal)) # True


