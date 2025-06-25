#include<iostream>
using namespace std;

int factorial(int x) {
    int result = 1;
    for (int i = 1; i <=x; i++) {
        result *= i;
    }
    return result;
}

int main() {

    cout << factorial(5) << "\n";
    cout << factorial(10) << "\n";
}




