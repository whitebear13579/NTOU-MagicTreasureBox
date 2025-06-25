#include<iostream>
using namespace std;

void modifyArray(int x[3]) {
    x[1] = 5;
}

int main(int argc, const char * argv[]) {

    int iarr[3] = {1, 2, 3};
    modifyArray(iarr);
    for (int x: iarr) {
        cout << x << endl;
    }       
}

