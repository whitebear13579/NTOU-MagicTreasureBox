#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    int *pi = new int(5);
    cout << *pi << '\n';
    delete pi;

    int *pia = new int[5];
    *pia = 3;
    *(pia+1) = 7;

    for (int i = 0; i < 5; i++) {
        cout << *(pia+i) << '\n';
    }
    delete [] pia;
}


