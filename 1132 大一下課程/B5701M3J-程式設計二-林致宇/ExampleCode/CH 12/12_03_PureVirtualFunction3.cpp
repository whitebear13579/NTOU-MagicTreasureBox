#include<iostream>
using namespace std;

class Shape {
public:
    virtual double area() = 0;
};

class Circle: public Shape {
public:
    double radius;
    double area() { return 3.14 * radius * radius; }
};

int main(int argc, const char * argv[]) {
    Shape s;
}









