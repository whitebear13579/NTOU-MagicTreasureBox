#include<iostream>
using namespace std;

int main() {

    for (int i = 1; i <= 5; i++) {
        if (i % 2 == 1) {
            continue;
        }
        for (int j = 0; j <= 5; j++) {
            if (j >= 3) {
                break;
            }
            cout << i << ", " << j << "\n";
        }        
    }
}
