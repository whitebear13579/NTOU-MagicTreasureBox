#include<iostream>
using namespace std;

class Car {
public:
    int color;
    ~Car();
};

Car::~Car() {
    cout << this->color << endl;
}

int main(int argc, const char * argv[]) {
    Car car1;
    car1.color = 5;
    if (true) {
        Car car2;
        car2.color = 3;
    }
}




