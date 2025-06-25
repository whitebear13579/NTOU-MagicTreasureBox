#include<iostream>
#include<cmath>
#define PI 3.141592653589793

using namespace std;

int main() {

    cout << "角度\tsin()\n";

    for (int a = 30; a <= 180; a += 30) {
        cout << a << "\t";
        cout << sin(a * PI / 100) << "\n";
    }
}




