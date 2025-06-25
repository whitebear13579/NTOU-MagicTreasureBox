#include<iostream>
using namespace std;

int main() {

    int i, j, k;
    i = 5 + (5 + 3) * 2;
    j = (5 + i) * 2 + 3 * i;
    k = (i + j) * (j - i);

    cout << i << ", " << j << ", " << k << '\n';
    return 0;
}
