#include<iostream>
using namespace std;

class Shape {
public:
    int x = 3;
    void showX() {
        cout << "(Shape) " <<  x << endl;
    }
};

class Circle: public Shape {
public:
    int x = 5;
    void showX() {
        cout << "(Circle) " <<  x << endl;
    }
};

int main(int argc, const char * argv[]) {
    Circle *c = new Circle;
    c->showX();
    Shape *s = new Circle;
    s->showX();
}






