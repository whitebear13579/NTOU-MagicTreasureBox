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
    Circle() { cout << "Circle Constructor" << endl; }
};

int main(int argc, const char * argv[]) {
    Circle c;
}










