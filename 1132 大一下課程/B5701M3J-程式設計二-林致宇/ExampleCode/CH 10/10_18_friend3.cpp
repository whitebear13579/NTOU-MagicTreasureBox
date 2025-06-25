#include<iostream>
using namespace std;

class Car;

class Person {
public:
    void myCar(Car c);
};

class Car {
private:
    friend class Person;
    int color = 5;
};

void Person::myCar(Car c) {
    cout << c.color << endl;
}

int main() {
    Car car;
    Person p;
    p.myCar(car);
}




