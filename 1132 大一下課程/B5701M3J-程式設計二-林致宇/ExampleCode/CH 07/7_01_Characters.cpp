#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello World!"};

    for (int i = 0; i < str1.length(); i++) {
        cout << str1[i] << endl;
    }
    for (int i = 0; i < str1.length(); i++) {
        cout << str1.at(i) << endl;
    }
    str1[0] = 'h';
    cout << str1 << endl;
}




