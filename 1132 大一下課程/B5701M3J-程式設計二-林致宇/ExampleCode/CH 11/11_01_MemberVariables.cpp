#include<iostream>
using namespace std;

class Shape {
private:
    int pri_member;
protected:
    int pro_member;
public:
};

class Circle: public Shape {
    void accessProMember() { pro_member = 6; }
    // error void accessPriMembeer() { pri_member = 5; } // error
};

int main(int argc, const char * argv[]) {
    Circle c;
    // c.pri_member = 5; // error
    // c.pro_member = 6; // error
}






