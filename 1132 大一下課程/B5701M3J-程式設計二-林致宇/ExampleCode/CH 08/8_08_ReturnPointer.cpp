#include<iostream>
using namespace std;

double *avg(int x, int y) {
    double result = (x+y)/2.0;
    return new double(result);
}

int main(int argc, const char * argv[]) {

    double *d = avg(5, 6);
    cout << *d << '\n';
}
