#include<iostream>
using namespace std;

class Car {
public:
    int color;
};

int main(int argc, const char * argv[]) {
    Car cars[3];
    cars[0].color = 5;
    cout << cars[0].color << endl;
}




