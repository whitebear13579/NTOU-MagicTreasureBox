#include<iostream>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    char str1[] = "Hello World";

    cout << strlen(str1) << "\n";

    char str2[80];
    strcpy(str2, str1);
    cout << str2 << "\n";

    cout << strcmp(str1, str2) << "\n";
}
