#include<iostream>
using namespace std;

int main(int argc, const char * args[]) {

    float f = 2.5f;
    double pi = 3.141592653589793;

    cout << f << " , " << pi << "\n";
    cout << sizeof(float) << " , "
        << sizeof(double) << " , "
        << sizeof(long double) << "\n";
    return 0;
}
