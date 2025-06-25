#include<iostream>
using namespace std;

class Car {
private:
    double gas;
public:
    Car(double gas) { this->gas = gas; }
    double getGas() { return gas; }
    void fillGas(double x) { gas += x; }
};

int main(int argc, const char * argv[]) {
    Car car1 {2.0};
    car1.fillGas(3.0);
    cout << car1.getGas() << endl;
}



