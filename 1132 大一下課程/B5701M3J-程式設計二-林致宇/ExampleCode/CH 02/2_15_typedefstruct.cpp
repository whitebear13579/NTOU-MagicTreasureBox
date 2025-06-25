#include<iostream>
using namespace std;
typedef unsigned char byte;

typedef struct point {
    int x;
    int y;
} Point;

int main(int argc, const char * argv[]) {

    Point p;
    p.x = 5;
    p.y = 3;
    cout << p.x << '\n';
    return 0;
}
