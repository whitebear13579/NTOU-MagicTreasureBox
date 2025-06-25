#include<iostream>
using namespace std;

int main() {

    int sum = 0;
    for (int i = 2; i <= 8; i = i + 2) {
        sum = sum + i;
    }
    cout << sum << "\n";
}
