#include<iostream>
using namespace std;

int main() {

    int sum = 0, i = 0, j = 0;
    for (i = 0; i <= 2; i++) {
        for (int j = 2; j >= 1; j--) {
            sum = sum + i + j;
        }
    }
    cout << sum << ", " << i << ", " << j << "\n";
}
