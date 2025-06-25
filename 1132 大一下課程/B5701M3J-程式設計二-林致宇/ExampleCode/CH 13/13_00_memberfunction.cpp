#include<iostream>
using namespace std;

class Rect {
public:
    double length;
    double width;
    bool operator<(const Rect& rect) const {
        return (length*width) < (rect.length*rect.width);
    }
};

int main(int argc, const char * argv[]) {
    Rect rect1; rect1.length = 3.0; rect1.width = 3.0;
    Rect rect2; rect2.length = 2.0; rect2.width = 5.0;
    if (rect1 < rect2) {
        cout << "Rect1 < Rect2" << endl;
    }
}




