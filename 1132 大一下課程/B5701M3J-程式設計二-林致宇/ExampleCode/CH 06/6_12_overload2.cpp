#include<iostream>
using namespace std;

double avg(int *x, int y) {
    cout << "Avg1" << endl;
    return (*x+y)/2.0;
}

double avg(double *x, int y) {
    cout << "Avg2" << endl;
    return (*x+y)/2.0;
}

int main() {
    int i = 6;
    double d = 3.0;
    cout << avg(&i, 7) << "\n";
    cout << avg(&d, 7) << "\n";
}




