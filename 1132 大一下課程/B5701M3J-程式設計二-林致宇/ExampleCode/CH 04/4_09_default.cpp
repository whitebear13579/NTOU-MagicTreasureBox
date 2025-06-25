#include<iostream>
using namespace std;

int main() {

    char c = 'Z';

    switch (c) {
        case 'A':
            cout << "Great\n";
            break;
        case 'B':
            cout << "Good\n";
            break;
        case 'C':
            cout << "OK\n";
            break;
        default:
            cout << c << "\n";
    }    
}

