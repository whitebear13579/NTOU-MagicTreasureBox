#include<iostream>
using namespace std;

bool comp(int data1, int data2, std::function<bool(int,int)> cmp) {
    return cmp(data1, data2);
}

class LessThan {
public:
    bool operator()(int a, int b) { return a < b; }
};

bool greaterThan(int a, int b) { return a > b; }

int main() {
    int a = 6, b = 8;
    LessThan lessthan;
    cout << comp(a, b, lessthan) << endl;
    cout << comp(a, b, greaterThan) << endl;
    cout << comp(a, b, [](int a, int b){ return a==b; }) << endl;
}






