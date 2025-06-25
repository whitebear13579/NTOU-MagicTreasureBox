#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello John!"};
    str1.replace(6, 4, "Bob");
    cout << str1 << endl;
}




