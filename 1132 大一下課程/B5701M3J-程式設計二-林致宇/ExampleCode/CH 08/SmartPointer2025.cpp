#include <memory>
#include <iostream>
class B;
class A {
public:
    std::shared_ptr<B> b_ptr;
    ~A() { std::cout << "A destroyed\n"; }
};
class B {
public:
    std::weak_ptr<A> a_ptr;
    ~B() { std::cout << "B destroyed\n"; }
};
int main() {
    auto a = std::make_shared<A>();
    auto b = std::make_shared<B>();
    a->b_ptr = b;
    b->a_ptr = a;
}






