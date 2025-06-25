#include<iostream>

using namespace std;

int * larger(int a, int b) {
    if (a > b) {
        return &a;
    } else {
        return &b;
    }
}

int main() {
}




