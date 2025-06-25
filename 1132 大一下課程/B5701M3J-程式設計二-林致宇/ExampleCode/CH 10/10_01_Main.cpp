#include<iostream>
#include "Car.h"
using namespace std;

int main(int argc, const char * argv[]) {
    Car car1;

    car1.color = 5;
    car1.fillGas(3.0);

    cout << car1.color << endl;
    cout << car1.getGas() << endl;
}
