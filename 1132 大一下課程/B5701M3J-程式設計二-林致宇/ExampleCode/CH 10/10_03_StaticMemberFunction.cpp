#include<iostream>
using namespace std;

class Car {
public:
    static int sv;
    int color;
    static void printVariables() {
        cout << sv << endl;
        // cout << color << endl;
    }
};
int Car::sv = 3;
int main(int argc, const char * argv[]) {
    Car::printVariables();
}


