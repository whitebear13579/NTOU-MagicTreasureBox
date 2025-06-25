#include<iostream>
#include<string>
using namespace std;

int main(int argc, const char * argv[]) {

    string str1 = string();
    str1 = "Hello";
    str1 += " World";
    cout << str1  << endl;
    str1.append("!");
    cout << str1  << endl;
    str1.assign("Hello Kitty");
    cout << str1 << endl;
}




