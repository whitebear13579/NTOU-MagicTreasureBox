#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    int i = 6;
    int &j = i; // j 和 i 是指到同一個記憶體空間, 類似取別名

    cout << j << '\n';
    j = 5;
    cout << i << '\n';    
}


