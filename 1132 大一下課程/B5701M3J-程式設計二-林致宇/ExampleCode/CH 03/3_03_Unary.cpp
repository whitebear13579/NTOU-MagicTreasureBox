#include<iostream>
using namespace std;

int main() {

    int i = 5, j = 7;

    cout << i++ << ", " << ++i << "\n";
    cout << --j << ", " << j-- << "\n";

    cout << i + (-j) << "\n";
    cout << i * (-j) << "\n";

    return 0;
}
