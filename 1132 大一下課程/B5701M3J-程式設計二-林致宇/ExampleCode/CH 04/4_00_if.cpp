#include<iostream>
using namespace std;

int main() {

    int i = 3, j = 5;
    if (i < j && j < 7) {
        cout << "Hello World\n";        
    }

    if (i < j) {
        if (j < 7) {
            cout << "Hello World\n";
        }
    }
    return 0;
}
