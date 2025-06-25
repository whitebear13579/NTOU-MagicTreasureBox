#include<iostream>
using namespace std;

int main(int argc, const char * args[]) {

    bool b1, b2, b3;
    b1 = true;
    b2 = 0;
    b3 = 3;

    cout << b1 << " , " << b2 << " , " << b3 << "\n";
    cout << boolalpha;
    cout << b1 << " , " << b2 << " , " << b3 << "\n";

    cout << "Sizeof: " << sizeof(bool) << "\n";
    return 0;
}
