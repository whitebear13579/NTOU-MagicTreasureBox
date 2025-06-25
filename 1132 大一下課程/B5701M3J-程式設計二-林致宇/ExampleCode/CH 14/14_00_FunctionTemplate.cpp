#include<iostream>
using namespace std;

template<class T>
int min(T data[], int size) {
    int index = 0;
    for (int i = 1; i < size; i++) {
        if (data[i] < data[index]) {
            index = i;
        }
    }
    return index; 
}

int main() {
    int idata[] = {68, 27, 32};
    double ddata[] = {3.6, 7.2, 1.3};

    cout << min<int>(idata, 3) << endl;
    cout << min<double>(ddata, 3) << endl;
}






