#include<iostream>
using namespace std;

int main() {

    int total {};    
    for (auto x: {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}) {
        total += x;
    }
    cout << total << endl;
}

