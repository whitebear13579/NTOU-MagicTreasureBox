#include<iostream>
using namespace std;

template<class T, int N>
int min(T data[N]) {
    int index = 0;
    for (int i = 1; i < N; i++) {
        if (data[i] < data[index]) {
            index = i;
        }
    }
    return index; 
}

int main() {
    int idata[3] = {68, 27, 32};
    double ddata[3] = {3.6, 7.2, 1.3};

    cout << min<int, 3>(idata) << endl;
    cout << min<double, 3>(ddata) << endl;
}








