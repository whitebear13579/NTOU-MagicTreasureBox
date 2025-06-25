#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello World!"};
    str1.erase(5, 5);
    cout << str1 << endl;

    string str2 {"Hello World"};
    cout << str2.erase(5) << endl;
    cout << str2.erase(1, 3) << endl;
    str2.clear();

    cout << str2.empty() << endl;
}




