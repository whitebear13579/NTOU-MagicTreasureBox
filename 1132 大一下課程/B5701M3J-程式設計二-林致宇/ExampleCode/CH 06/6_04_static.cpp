#include<iostream>
#include "6_03_f.h"
using namespace std;

int f(int x) {
    static int y = x;
    y++;
    return y;
}

int main() {

    cout << f(5) << "\n";
    cout << f(3) << "\n";
}

