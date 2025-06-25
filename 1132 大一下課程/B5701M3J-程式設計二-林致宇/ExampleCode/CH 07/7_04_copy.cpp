#include<iostream>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    char str1[3] = "Hi";
    char str2[6] = "World";

    cout << str1 << ", " << sizeof(str1) << "\n";
    cout << str2 << ", " << sizeof(str2) << "\n";

    strcpy(str1, str2);

    cout << str1 << ", " << sizeof(str1) << "\n";
    cout << str2 << ", " << sizeof(str2) << "\n";

}
