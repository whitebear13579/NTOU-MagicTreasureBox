#include<iostream>
using namespace std;

int main() {

    int sum = 0;
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            sum = sum + i + j;
        }
    }
    cout << sum << "\n";
}
