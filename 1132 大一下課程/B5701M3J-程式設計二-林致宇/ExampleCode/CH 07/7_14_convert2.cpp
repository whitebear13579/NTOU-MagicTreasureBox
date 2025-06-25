#include<iostream>
#include<string>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    string str = "Hello World!";

    int len = (int)str.size();
    char *cstr = new char[len+1];

    strcpy(cstr, str.c_str());
    cout << cstr << endl;
}



