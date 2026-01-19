# *args 範例：允許傳入任意數量的數字來加總
# *args 會將傳入的參數打包成一個 tuple (永遠都是 tuple)
def calculate_sum(*numbers):
    """Calculate sum of any number of arguments"""
    print(f"Received arguments: {numbers}") # numbers 是一個 tuple
    total = sum(numbers)
    return total

print(calculate_sum(1, 2, 3)) # Received arguments: (1, 2, 3) -> 6
print(calculate_sum(10, 20, 30, 40)) # Received arguments: (10, 20, 30, 40) -> 100

# **kwargs 範例：允許傳入任意數量的關鍵字屬性
# **kwargs 會將傳入的參數打包成一個 dict (永遠都是 dict)
def create_user_profile(name, **details):
    """Create user profile with flexible details"""
    print(f"User: {name}")
    print("Details:")
    # details 是一個 dict
    for key, value in details.items():
        print(f"  {key}: {value}")

create_user_profile("Alice", age=25, city="Taipei", job="Engineer")
create_user_profile("Bob", age=30, country="Taiwan")