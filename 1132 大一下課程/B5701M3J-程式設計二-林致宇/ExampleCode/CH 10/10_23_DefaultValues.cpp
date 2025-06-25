#include<iostream>
using namespace std;

class Time {
public:
    int hour, min, sec;
    Time(int, int, int);
    void show() {
        cout << hour << ", " << min << ", " << sec << endl;
    }
};

Time::Time(int h, int m = 1, int s = 1) {
    this->hour = h;
    this->min = m;
    this->sec = s;
}

int main(int argc, const char * argv[]) {
    Time t1(12); Time t2(6, 8); Time t3(1, 6, 8);
    t1.show(); t2.show(); t3.show();
}





