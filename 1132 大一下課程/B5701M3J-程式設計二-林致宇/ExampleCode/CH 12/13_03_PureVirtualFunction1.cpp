#include<iostream>
using namespace std;

class Shape {
public:
    virtual double area() = 0;
};

class Circle: public Shape {
public:
};

int main(int argc, const char * argv[]) {
    Circle c;
}







