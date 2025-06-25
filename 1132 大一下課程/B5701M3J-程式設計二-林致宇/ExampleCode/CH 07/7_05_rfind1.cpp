#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"abcdefabcxyzdef"};

    int x = str1.rfind("def", string::npos);
    int y = str1.rfind("def", x-1);

    cout << x << " , " << y << endl;
}




