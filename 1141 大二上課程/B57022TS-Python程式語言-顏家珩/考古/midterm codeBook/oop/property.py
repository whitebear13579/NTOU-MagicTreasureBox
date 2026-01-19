class Person:
    def __init__(self, age: int):
        self._age = None          # 真正儲存放在「私有欄位」
        self.age = age            # 走 setter 做一次驗證

    
    @property
    def age(self) -> int:         # getter：讀取時會被呼叫
        return self._age

    @age.setter
    def age(self, v: int):        # setter：賦值時會被呼叫
        if not isinstance(v, int):
            raise TypeError("age 必須是 int")
        if v < 0 or v > 150:
            raise ValueError("age 超出合理範圍")
        self._age = v

    @property
    def birth_year(self):      # 計算屬性
        from datetime import datetime
        return datetime.now().year - self._age

    @property
    def is_adult(self):        # 唯讀
        return self._age >= 18

p = Person(20)
p.age        # -> 20   （call getter）
p.age = 30   # -> 30   （call setter ）
p.age = -5   # ValueError: age 超出合理範圍