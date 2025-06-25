#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello World!"};
    string str2 = str1.substr(6, 5);

    cout << str2 << endl;
}



