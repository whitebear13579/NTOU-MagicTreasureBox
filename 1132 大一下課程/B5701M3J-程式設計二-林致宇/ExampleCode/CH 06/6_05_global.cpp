#include<iostream>
#include "6_03_f.h"
using namespace std;

int y = 3;

int f(int x) {
    y = x;
}

int main() {
    cout << y << "\n";
    f(5);
    cout << y << "\n";
}

