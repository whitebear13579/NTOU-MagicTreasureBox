#include<iostream>
using namespace std;

class A {
public:
    int x;
};
class B1: public virtual A {};
class B2: public virtual A {};
class B3: public A {};
class C: public B1, public B2, public B3 {};

int main() {
    C c;
    c.B1::x = 1;
    c.B2::x = 2;
    c.B3::x = 3;
    cout << c.B1::x << ", "
        << c.B2::x << ", "
        << c.B3::x << endl;
}















