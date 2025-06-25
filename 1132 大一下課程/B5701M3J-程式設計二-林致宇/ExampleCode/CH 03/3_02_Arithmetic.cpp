#include<iostream>
using namespace std;

int main() {

    int i, j, k;
    i = j = (k = 3) + 5; // Right-to-left

    cout << i << ", " << j << ", " << k << '\n';
    return 0;
}
