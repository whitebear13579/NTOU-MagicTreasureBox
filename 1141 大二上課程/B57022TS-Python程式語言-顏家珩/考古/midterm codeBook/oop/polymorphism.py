class Shape:
    def area(self): return 0
    def perimeter(self): return 0

class Rectangle(Shape):
    def __init__(self, w, h): self.w, self.h = w, h
    def area(self): return self.w * self.h
    def perimeter(self): return 2 * (self.w + self.h)

class Circle(Shape):
    def __init__(self, r): self.r = r
    def area(self):
        import math
        return math.pi * self.r ** 2
    def perimeter(self):
        import math
        return 2 * math.pi * self.r
