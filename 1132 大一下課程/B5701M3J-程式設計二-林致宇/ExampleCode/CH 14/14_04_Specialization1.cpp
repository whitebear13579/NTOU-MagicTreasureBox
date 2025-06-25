#include<iostream>
#include <vector>
using namespace std;

template<class T>
class Equal {
private:
public:
    bool check(T data1, T data2) {
        if (data1 == data2) {
            return true;
        } else {
            return false;
        }
    }
};

int main() {
    cout << boolalpha;
    int i1 = 6, i2 = 8, i3 = 8;
    Equal<int> e1;
    cout << e1.check(i1, i2) << endl;
    cout << e1.check(i2, i3) << endl;
    Equal<double> e2;
    cout << e2.check(5.0, 3.0) << endl;
    cout << e2.check(5.0, 5.0) << endl;
    Equal<int *> e3;
    cout << e3.check(&i1, &i2) << endl;
    cout << e3.check(&i2, &i3) << endl;
}











