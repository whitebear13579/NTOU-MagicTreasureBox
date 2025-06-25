#include<iostream>
#include<string>
using namespace std;

int main(int argc, const char * argv[]) {

    string str1 = "Hello World!";
    string str2 = "";

    cout << str1.capacity() << endl;
    cout << str1.max_size() << endl;
    cout << str2.capacity() << endl;
    cout << boolalpha << str2.empty() << endl;

    str1.reserve(30);
    cout << str1.capacity() << endl;
    str1.reserve(5);
    cout << str1 << " , " << str1.capacity() << endl;
    str1.resize(5);
    cout << str1 << " , " << str1.capacity() << endl;
    str1.resize(30);
    cout << str1 << " , " << str1.capacity() << endl;
}



