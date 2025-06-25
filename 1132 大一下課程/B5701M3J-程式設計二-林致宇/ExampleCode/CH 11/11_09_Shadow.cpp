#include<iostream>
using namespace std;

class Shape {
public:
    int x;
};

class Circle: public Shape {
public:
    double x;
    void showX() {
        cout << x << ", " << Shape::x << endl;
    }
};

int main(int argc, const char * argv[]) {
    Circle c;
    c.x = 5;
    c.Shape::x = 7;
    c.showX();
}












