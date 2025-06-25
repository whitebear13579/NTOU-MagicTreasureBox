#include<iostream>
using namespace std;

class A {
public:
    void f() { cout << "A" << endl; }
};
class B1: public virtual A {
public:
    void f() { cout << "B1" << endl; }
};
class B2: public virtual A {};
class C: public B1, B2 {};

int main() {
    C c;
    c.f();
    c.A::f();
}















