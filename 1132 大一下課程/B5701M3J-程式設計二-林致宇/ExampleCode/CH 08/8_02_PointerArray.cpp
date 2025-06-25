#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    double da[] = {1.0, 2.0, 3.0};
    double *p1 = da;
    double *p2 = p1;

    cout << *(da+1) << '\n';
    cout << p1[2] << '\n';
    cout << *(p1+1) << '\n';

    *(p2 +1) = 5.0;
    cout << p1[1] << '\n';
}


