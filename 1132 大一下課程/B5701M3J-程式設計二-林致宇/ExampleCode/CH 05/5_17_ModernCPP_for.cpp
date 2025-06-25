#include<iostream>
using namespace std;

int main() {

    int total {};
    int iarray[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};     
    for (int x: iarray) {
        total += x;
    }
    cout << total << endl;
}

