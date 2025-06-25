#include<iostream>
using namespace std;

class A {
public:
    int x;
};
class B1: private virtual A {};
class B2: public virtual A {};
class C: public B2, B1 {};

int main() {
    C c;
    c.x = 3;
}















