#include<iostream>
using namespace std;

class Rect {
public:
    int length;
    int width;
    Rect & setLength(int length)  {
        this->length = length;
        return *this;
    }
    Rect & setWidth(int width) {
        this->width = width;
        return *this;
    }
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << width << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1;
    rect1.setLength(6).setWidth(8);
    rect1.showRectSize();
}






