#include<iostream>
using namespace std;

class Vehicle {
public:
    Vehicle() { cout << "Vehicle" << endl; }
};

class ExerciseTool {
public:
    ExerciseTool() { cout << "ExerciseTool" << endl; }
};

class Bicycle: public ExerciseTool, public Vehicle {
public:
    Bicycle() { cout << "Bicycle" << endl; }
};

int main(int argc, const char * argv[]) {
    Bicycle bike;
}













