#include<iostream>
using namespace std;

class I {
public:
    int number;
    operator int() { return number; }
};

int main() {
    I i;
    i.number = 7;
    int j = (int)i + 3;
    cout << j << endl;
}





