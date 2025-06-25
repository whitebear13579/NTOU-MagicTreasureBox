#include<iostream>
#include<iomanip>
using namespace std;

int main() {

    const int maxn = 10;
    cout << setw(8) << "integer" << setw(8) << "sum"
        << setw(20) << "factorial" << endl;
    
    for (int i {1}, sum {}, factorial{1}; i <= maxn; i++) {
        sum += i;
        factorial *= i;
        cout << setw(8) << i << setw(8) << sum
            << setw(20) << factorial << endl;
    }
}

