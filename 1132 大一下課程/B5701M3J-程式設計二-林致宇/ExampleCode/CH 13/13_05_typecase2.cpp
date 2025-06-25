#include<iostream>
using namespace std;

class Rect {
public:
    double length;
    double width;
    operator double() const {
        return (length*width);
    }
};

int main(int argc, const char * argv[]) {
    double d1 = 3.0;
    Rect rect1; rect1.length = 3.0; rect1.width = 3.0;
    double d2 = d1 + static_cast<double>(rect1);
    cout << d2 << endl;
}




