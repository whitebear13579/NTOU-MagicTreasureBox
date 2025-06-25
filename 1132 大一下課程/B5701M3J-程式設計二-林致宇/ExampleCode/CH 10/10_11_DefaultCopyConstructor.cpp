#include<iostream>
using namespace std;

class Rect {
private:
    int length;
    int width;
public:
    Rect(int l, int w): length {l}, width {w} {}
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << width << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1 {6, 8};
    Rect rect2 = rect1;
    Rect rect3 = Rect(rect1);
    rect1.showRectSize();
    rect2.showRectSize();
    rect3.showRectSize();
    cout << &rect1 << ", " << &rect2 << ", " << &rect3;
}



