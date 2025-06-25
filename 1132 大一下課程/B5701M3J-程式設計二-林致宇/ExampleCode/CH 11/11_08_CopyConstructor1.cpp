#include<iostream>
using namespace std;

class Shape {
public:
    int x;
};

class Circle: public Shape {
public:
    int y;
};

int main(int argc, const char * argv[]) {
    Circle c1;
    c1.x = 5;
    c1.y = 3;
    Circle c2 = c1;
    cout << c2.x << ", " << c2.y << endl;
}









