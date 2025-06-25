#include<iostream>
using namespace std;

template<typename T, typename Comp>
bool comp(T data1, T data2, Comp cmp) {
    return cmp(data1, data2);
}

class LessThan {
public:
    bool operator()(int a, int b) { return a < b; }
};

template <typename T>
bool greaterThan(T a, T b) { return a > b; }

int main() {
    int a = 6, b = 8;
    LessThan lessthan;
    cout << comp(a, b, lessthan) << endl;
    cout << comp(a, b, greaterThan<int>) << endl;
}






