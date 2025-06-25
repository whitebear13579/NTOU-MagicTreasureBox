#include<iostream>
using namespace std;

int main() {

    for (int i = 1; i <= 10; i++) {
        if (i % 2 == 1) {
            continue;
        }
        for (int j = 0; j <= 10; j++) {
            if (j % 2 == 0) {
                continue;
            }
            cout << i << ", " << j << "\n";
        }        
    }
}
