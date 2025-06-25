#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"abcdefabcxyzdef"};

    int x = str1.find("abc", 0);
    int y = str1.find("abc", x+3);

    cout << x << " , " << y << endl;
}




