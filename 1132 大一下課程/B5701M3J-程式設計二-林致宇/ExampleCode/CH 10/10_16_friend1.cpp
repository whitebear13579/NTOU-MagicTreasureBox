#include<iostream>
using namespace std;

class Car {
private:
    int color = 5;
    friend void printColor(Car);
};

void printColor(Car c) {
    cout << c.color << endl;
}

int main() {
    Car car;
    printColor(car);
}




