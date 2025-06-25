#include<iostream>
using namespace std;

class A {
public:
    int x;
};
class B: public virtual A {    
};
class C: public virtual A {    
};
class D: public B, public C {
};

int main() {
    D d;
    d.B::x = 3;
    d.C::x = 5;
    cout << d.B::x << ", " << d.C::x << endl;
}















