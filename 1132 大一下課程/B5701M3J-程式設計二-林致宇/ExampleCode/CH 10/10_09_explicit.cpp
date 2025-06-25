#include<iostream>
using namespace std;

class Car {
private:
    double gas;
public:
    explicit Car(double gas): gas {gas}{}
    void fillGas(Car c) { gas += c.gas; }
};

int main(int argc, const char * argv[]) {
    Car car1 {2.0};
    car1.fillGas(3.0);
}



