#include<iostream>
using namespace std;

class Car {
private:
    double gas;
public:
    int color;
    double getGas() { return gas; }
    void fillGas(double x) { gas += x; }
};

int main(int argc, const char * argv[]) {
    Car car1;

    car1.color = 5;
    // car1.gas = 3.0;
    car1.fillGas(3.0);

    cout << car1.color << endl;
    cout << car1.getGas() << endl;
}



