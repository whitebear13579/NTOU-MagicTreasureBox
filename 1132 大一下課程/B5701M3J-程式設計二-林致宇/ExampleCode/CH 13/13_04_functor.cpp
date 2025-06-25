#include<iostream>
using namespace std;

class CircleArea {
public:
    double operator()(double r){
        return (3.14*r*r);
    }
};

void getCircleArea(double r, CircleArea ca) {
    double area = ca(r);
    cout << area << endl;
}

int main(int argc, const char * argv[]) {
    CircleArea ca;
    getCircleArea(2.0, ca);    
}





