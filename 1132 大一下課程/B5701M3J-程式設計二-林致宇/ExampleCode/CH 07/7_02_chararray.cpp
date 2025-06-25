#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    char s1[] = "Hello";
    char s2[] = {'H', 'e', 'l', 'l', 'o'};

    cout << s1 << ", " << sizeof(s1) << "\n";
    cout << s2 << ", " << sizeof(s2) << "\n";
}
