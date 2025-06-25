#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello World"};
    string str2 {"Hello"};

    cout << str1.compare(0, 5, str2) << endl;
}






