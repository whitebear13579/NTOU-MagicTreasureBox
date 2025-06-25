#include<iostream>
using namespace std;

class Rect {
public:
    int length;
    int *width;
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << *width << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1;
    rect1.length = 6;
    rect1.width = new int {8};
    rect1.showRectSize();
}





