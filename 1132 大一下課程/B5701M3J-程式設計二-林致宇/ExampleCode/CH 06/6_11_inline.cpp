#include<iostream>
using namespace std;

inline int f(int x) {
    return (x * x + 3 * x + 2);
}

int main() {

    int x = 5;
    
    cout << f(x+1) << "\n";
    cout << f(6) << "\n";
}




