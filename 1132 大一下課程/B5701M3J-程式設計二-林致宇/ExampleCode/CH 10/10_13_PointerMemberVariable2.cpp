#include<iostream>
using namespace std;

class Rect {
public:
    int length;
    shared_ptr<int> pWidth;
    void showRectSize() {
        cout << "Length: " << length << endl;
        cout << "Width: " << *pWidth << endl;
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1;
    rect1.length = 6;
    rect1.pWidth = make_shared<int>(8);
    rect1.showRectSize();
}






