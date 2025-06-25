#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str {"Hello World! Hello Kitty! Hello Baby!"};
    string target = "Hello";

    unsigned int count = 0, pos = str.length();
    string::size_type i;

    while((i=str.rfind(target,pos)) != string::npos) {
        count++;
        cout << "(" << count << ") " << i << endl;
        if (i == 0) {
            break;
        }
        pos = (unsigned int) i - 1;
    }
}




