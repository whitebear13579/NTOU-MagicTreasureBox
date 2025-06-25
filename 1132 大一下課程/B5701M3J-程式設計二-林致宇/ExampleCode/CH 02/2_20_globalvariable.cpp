#include<iostream>
using namespace std;

int x = 5;
int y = 3;

int main(int argc, const char * args[]) {

    int x = 8;
    int y = 6;

    cout << ::x << " , " << ::y << endl;
    cout << x << " , " << y << endl;
}

