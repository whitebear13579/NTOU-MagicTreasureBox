#include<iostream>
using namespace std;

int main() {

    int i = 5, j = 5, k = 3, l = 7;

    cout << !(i < j) << "\n";
    cout << !(i <= j) << "\n";
    cout << boolalpha;
    cout << !(i <= j) << "\n";
    cout << !(i != j) << "\n";
    cout << !((i != j) == (k > l)) << "\n";    
    return 0;
}

