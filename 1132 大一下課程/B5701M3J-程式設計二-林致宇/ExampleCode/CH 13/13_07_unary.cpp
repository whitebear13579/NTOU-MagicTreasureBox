#include<iostream>
using namespace std;

class N {
public:
    int number;

    bool operator!() {
        if (number%2 == 0) { return 0; }
        else { return 1; }
    }
};

int main(int argc, const char * argv[]) {
    N n;
    n.number = 7;
    cout << !n << endl;
    n.number = 6;
    cout << !n << endl;
}





