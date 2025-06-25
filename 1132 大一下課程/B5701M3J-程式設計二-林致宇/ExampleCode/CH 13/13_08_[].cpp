#include<iostream>
#include<cassert>
using namespace std;

class I {
public:
    int number;
    int operator[](int i);
};

int I::operator[](int i) {
    assert(i >= 0);
    int x = 10;
    for (int j = 0; j < i; j++) {
        x *= 10;
    }
    int result = number%x;
    result = result/(x/10);
    return result;
}

int main() {
    I i;
    i.number = 57013;
    for (int j = 0; j < 5; j++) {
        cout << i[j] << "\t";
    }
    cout << endl;
    cout << i[5] << endl;
    cout << i[-1] << endl;
}





