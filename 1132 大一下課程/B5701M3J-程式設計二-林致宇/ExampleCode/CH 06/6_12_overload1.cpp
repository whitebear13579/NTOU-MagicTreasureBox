#include<iostream>
using namespace std;

double avg(int x, int y) {
    return (x+y)/2.0;
}

double avg(int x, int y, int z) {
    return (x+y+z)/3.0;
}

int main() {

    cout << avg(6, 7) << "\n";
    cout << avg(6, 7, 8) << "\n";
}




