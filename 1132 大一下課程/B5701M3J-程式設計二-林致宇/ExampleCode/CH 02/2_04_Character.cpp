#include<iostream>
using namespace std;

int main(int argc, const char * args[]) {

    char c1 = 'B';
    char c2 = 65;
    wchar_t c3 = 'C';

    cout << c1 << " , " << c2 << " , " << c3 << "\n";
    cout << (int)c1 << "\n";
    cout << sizeof(char) << " , " << sizeof(wchar_t) << "\n";

    return 0;
}
