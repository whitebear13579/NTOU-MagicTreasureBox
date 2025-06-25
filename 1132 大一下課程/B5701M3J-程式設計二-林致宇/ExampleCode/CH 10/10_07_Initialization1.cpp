#include<iostream>
using namespace std;

class Point {
public:
    int x, y;
    void showPoint() { cout << "("
        << x << ", " << y << ")" << endl; }
};

int main(int argc, const char * argv[]) {
    Point p1 = {6, 8};
    p1.showPoint();
}



