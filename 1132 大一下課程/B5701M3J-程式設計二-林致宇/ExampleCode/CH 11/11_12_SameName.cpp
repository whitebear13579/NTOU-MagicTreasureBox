#include<iostream>
using namespace std;

class Vehicle {
public:
    double speed;
    double getSpeed() { return speed; }
};

class ExerciseTool {
public:
    double speed;
    double getSpeed() { return speed; }
};

class Bicycle: public ExerciseTool, public Vehicle {
};

int main(int argc, const char * argv[]) {
    Bicycle bike;
    bike.Vehicle::speed = 3.0;
    bike.ExerciseTool::speed = 5.0;
    cout << bike.Vehicle::getSpeed() << endl;
    cout << bike.ExerciseTool::getSpeed() << endl;
}














