#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str {"Hi World! Hi Kitty! Hi Baby!"};
    string ostr {"Hi"};
    string rstr {"Hello"};
    string::size_type i;
    unsigned int pos = 0;

    while((i = str.find(ostr, pos)) != string::npos) {
        str.replace(i, ostr.length(), rstr);
        pos = i + 1;
    }
    cout << str << endl;
}




