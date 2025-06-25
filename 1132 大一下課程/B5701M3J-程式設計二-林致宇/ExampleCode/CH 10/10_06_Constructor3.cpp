#include<iostream>
using namespace std;

class Car {
private:
    double gas;
public:
    Car(double g) { gas = g; }
    Car() = default;
    double getGas() { return gas; }
    void fillGas(double x) { gas += x; }
};

int main(int argc, const char * argv[]) {
    Car car1;
}



