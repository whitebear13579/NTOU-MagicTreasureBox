#include<iostream>
using namespace std;

class Car {
public:
    static int color;
};

int Car::color = 3;

int main(int argc, const char * argv[]) {
    Car car1, car2;
    cout << car1.color << ", " << car2.color << endl;
    car1.color = 5;
    cout << car1.color << ", " << car2.color << endl;
}

