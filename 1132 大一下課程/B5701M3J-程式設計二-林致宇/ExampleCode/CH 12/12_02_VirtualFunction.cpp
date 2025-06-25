#include<iostream>
using namespace std;

class Shape {
public:
    virtual double area();
};
double Shape::area() { return 0; }

class Circle: public Shape {
public:
    double radius;
    double area() {
        return 3.14 * radius * radius;
    }
};
class Squre: public Shape {
public:
    double side;
    double area() { return side * side; }
};

double getTotalPrice(Shape *s) {
    return s->area()*2.0;
}

int main(int argc, const char * argv[]) {
    Circle c;
    c.radius = 2.0;
    cout << getTotalPrice(&c) << endl;;
    Squre s;
    s.side = 2.0;
    cout << getTotalPrice(&s) << endl;;
}







