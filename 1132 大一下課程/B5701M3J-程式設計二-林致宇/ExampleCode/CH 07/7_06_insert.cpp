#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello!"};
    string str2 {" World"};
    str1.insert(5, str2);
    cout << str1 << endl;
}




