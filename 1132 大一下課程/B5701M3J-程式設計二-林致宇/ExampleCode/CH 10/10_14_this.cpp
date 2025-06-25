#include<iostream>
using namespace std;

class Rect {
public:
    int length;
    int width;
    void setLengthWidth(int length, int width) {
        this->length = length;
        this->width = width;
    }
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << width << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1;
    rect1.setLengthWidth(6, 8);
    rect1.showRectSize();
}






