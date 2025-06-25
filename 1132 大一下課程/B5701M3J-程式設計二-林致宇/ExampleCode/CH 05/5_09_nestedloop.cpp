#include<iostream>
using namespace std;

int main() {

    int sum = 0, i = 0, j = 0;
    for (i = 5; i <= 8; i = i + 2) {
        for (int j = 9; j >= 5; j = j -3) {
            sum = sum + i - j;
        }
    }
    cout << sum << ", " << i << ", " << j << "\n";
}
