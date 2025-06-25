#include<iostream>
using namespace std;

template<typename T> T average(T a, T b);

int main() {
    cout << average(6, 9) << endl;
    cout << average(3.6, 5.8) << endl;
}

template<typename T> T average(T a, T b) {
    return (a+b)/2;
}







