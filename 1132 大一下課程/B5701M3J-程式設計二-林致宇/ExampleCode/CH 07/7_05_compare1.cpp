#include<iostream>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    char str1[] = "Hello";
    char str2[] = "Hello";
    char str3[] = "hello";

    cout << boolalpha;
    cout << (str1 == str2) << "\n";

    cout << strcmp(str1, str2) << "\n";
    cout << strcmp(str1, str3) << "\n";
}


