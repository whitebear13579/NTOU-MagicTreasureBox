#include<iostream>
using namespace std;

inline int sum(int x, int y) {
    return (x+y);
}

int main() {

    int x = 5, y = 3;
    
    cout << sum(x, y) << "\n";
    cout << sum(7, 6) << "\n";
}

