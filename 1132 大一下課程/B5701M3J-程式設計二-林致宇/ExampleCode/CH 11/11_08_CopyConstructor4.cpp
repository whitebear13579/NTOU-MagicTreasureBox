#include<iostream>
using namespace std;

class Shape {
public:
    int x;
    Shape() {}
    Shape(const Shape &s);
};

Shape::Shape(const Shape &s) {
    this->x = s.x + 2;
}

class Circle: public Shape {
public:
    Circle() {}
    Circle(const Circle &c);
    int y;
};

Circle::Circle(const Circle &c): Shape(c) {
    this->y = c.y + 3;
}

int main(int argc, const char * argv[]) {
    Circle c1;
    c1.x = 5;
    c1.y = 3;
    Circle c2 = c1;
    cout << c2.x << ", " << c2.y << endl;
}









