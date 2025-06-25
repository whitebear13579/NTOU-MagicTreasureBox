#include<iostream>
using namespace std;

class Person {
public:
    int money;
    Person(int m) {money = m;}
    Person operator+(const Person& p) const {
        return Person(money+p.money);
    }
};

int main(int argc, const char * argv[]) {
    Person p1(5); Person p2(3); Person p3(2);
    Person p = p1 + p2 + p3;
    cout << p.money << endl;
}




