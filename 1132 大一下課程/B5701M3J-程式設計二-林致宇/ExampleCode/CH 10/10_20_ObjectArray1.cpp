#include<iostream>
using namespace std;

class Car {
public:
    int color;
};

int main(int argc, const char * argv[]) {
    Car car1, car2, car3;
    car2.color = 6;
    Car cars[3] {car1, car2, Car()};
    cout << cars[1].color << endl;
    cars[2].color = 8;
}




