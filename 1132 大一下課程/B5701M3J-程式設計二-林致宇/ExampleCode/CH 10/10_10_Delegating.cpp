#include<iostream>
using namespace std;

class Rect {
private:
    double length;
    double width;
public:
    Rect(double l, double w): length {l}, width {w} {}
    Rect(double side): Rect {side, side} {}
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << width << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1 {3.0};
    rect1.showRectSize();
}



