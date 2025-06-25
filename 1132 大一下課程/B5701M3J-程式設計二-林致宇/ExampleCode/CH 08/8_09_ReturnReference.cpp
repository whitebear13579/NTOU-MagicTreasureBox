#include<iostream>
using namespace std;

double & avg(int x, int y) {
    double *result = new double((x+y)/2.0);
    cout << "$$ " << result << '\n';
    double &ref = *result;
    cout << "## " << &ref << '\n';
    cout << "&& " << ref << '\n';
    return ref;
}

int main(int argc, const char * argv[]) {
    double & d = avg(5, 6);
    cout << d << '\n';
    cout << "@@ " << &d << '\n';
}

