# 位置參數 (Positional Arguments)
# 必須按照正確的順序提供參數
def introduce(name, age, city):
    print(f"Hi, I'm {name}, {age} years old, from {city}")
    introduce("Alice", 25, "Taipei")
introduce("Alice", 25, "Taipei")

# 關鍵字參數 (Keyword Arguments)
# 使用 name=value 的方式傳入，順序不拘 。
def create_profile(name, age, city, occupation):
    """Create user profile using keyword arguments"""
    print(f"Profile: {name}, {age}, {city}, {occupation}")

create_profile(name="Bob", city="Kaohsiung", age=30, occupation="Engineer")
create_profile(age=28, name="Carol", occupation="Designer", city="Taichung")
# 混合使用位置參數與關鍵字參數，這時關鍵字參數必須放在位置參數之後
create_profile("David", 35, city="Tainan", occupation="Teacher")
