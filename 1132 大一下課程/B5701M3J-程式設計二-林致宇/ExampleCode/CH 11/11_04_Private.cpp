#include<iostream>
using namespace std;

class Shape {
private:
    int pri_member;
protected:
    int pro_member;
public:
    int pub_member;
};

class Circle: private Shape {
    void accessProMember() { pro_member = 6; }
};

int main(int argc, const char * argv[]) {
    Circle c;
    // c.pub_member = 5; // error
}








