#include<iostream>
using namespace std;

class I {
public:
    int number;
    I operator=(const int i);
};

I I::operator=(const int i) {
    this->number = i;
    return *this;
}

int main(int argc, const char * argv[]) {
    I i;
    i = 7;
    cout << i.number << endl;
}







