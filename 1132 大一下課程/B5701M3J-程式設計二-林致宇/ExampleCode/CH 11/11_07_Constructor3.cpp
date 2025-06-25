#include<iostream>
using namespace std;

class Shape {
public:
    double area;
    Shape(int x) { cout << x << endl; }
};

class Circle: public Shape {
public:
    double radius;
    Circle(): Shape(5) { cout << "Circle Constructor" << endl; }
    Circle(int x, double r): Shape(x), radius(r) {}
};

int main(int argc, const char * argv[]) {
    Circle c1;
    Circle c2(6, 3.0);
    Circle c3(8); // error
}










