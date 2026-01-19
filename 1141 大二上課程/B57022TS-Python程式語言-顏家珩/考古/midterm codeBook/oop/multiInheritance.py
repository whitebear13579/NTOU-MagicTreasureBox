class Flyable:  # mixin
    def fly(self): return f"{self.name} is flying!"

class Swimmable:  # mixin
    def swim(self): return f"{self.name} is swimming!"

class Animal:
    def __init__(self, name): self.name = name
    def eat(self): return f"{self.name} is eating"

class Duck(Animal, Flyable, Swimmable):
    def __init__(self, name): super().__init__(name)
    def quack(self): return f"{self.name} says Quack!"

duck = Duck("Donald")
print(duck.eat()) # From Animal
print(duck.fly()) # From Flyable
print(duck.swim()) # From Swimmable
print(duck.quack()) # From Duck