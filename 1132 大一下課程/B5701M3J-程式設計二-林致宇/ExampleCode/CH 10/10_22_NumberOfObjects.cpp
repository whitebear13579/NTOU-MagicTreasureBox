#include<iostream>
using namespace std;

class Car {
public:
    static int counter;
    Car() { counter++; }
};

int Car::counter = 0;

int main(int argc, const char * argv[]) {
    Car car1, car2;
    cout << Car::counter << endl;
}




