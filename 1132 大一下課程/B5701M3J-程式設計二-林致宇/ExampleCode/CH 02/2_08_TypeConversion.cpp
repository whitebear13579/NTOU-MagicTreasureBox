#include<iostream>
using namespace std;

int main(int argc, const char * args[]) {

    int i1 = 3;
    double d1 = 2.2;

    cout << i1 + d1 << "\n";
    cout << i1 + (int)d1 << "\n";
    cout << 5/3 << "\n";
    cout << 5/(double)3 << "\n";
    cout << (double)(5/3) << "\n";
    return 0;
}
