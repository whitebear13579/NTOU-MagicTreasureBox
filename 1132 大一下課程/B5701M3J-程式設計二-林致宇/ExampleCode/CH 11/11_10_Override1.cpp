#include<iostream>
using namespace std;

class Shape {
public:
    double area;
    void draw() { cout << "1: draw()" << endl; }
};

class Circle: public Shape {
public:
    double radius;
    void draw() { cout << "2: draw()" << endl; }
};

int main(int argc, const char * argv[]) {
    Circle c;
    c.draw();
}












