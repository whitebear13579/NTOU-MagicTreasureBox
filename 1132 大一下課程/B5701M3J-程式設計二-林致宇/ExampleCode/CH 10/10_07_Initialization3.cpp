#include<iostream>
using namespace std;

class Point {
private:
    int x, y;
public:
    Point(int x, int y): x (x), y (y) {};
    void showPoint() { cout << "("
        << x << ", " << y << ")" << endl; }
};

int main(int argc, const char * argv[]) {
    Point p1(6, 8);
    p1.showPoint();
}



