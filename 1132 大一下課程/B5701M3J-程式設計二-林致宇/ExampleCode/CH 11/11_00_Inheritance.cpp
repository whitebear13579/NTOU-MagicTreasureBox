#include<iostream>
using namespace std;

class Shape {
public:
    double area;
    void draw() {
        cout << "draw() is called." << endl;
    }
};

class Circle: public Shape {
public:
    double radius;
};

int main(int argc, const char * argv[]) {
    Circle c;
    c.radius = 2.0;
    c.area = c.radius*c.radius*3.14;
    c.draw();
}






