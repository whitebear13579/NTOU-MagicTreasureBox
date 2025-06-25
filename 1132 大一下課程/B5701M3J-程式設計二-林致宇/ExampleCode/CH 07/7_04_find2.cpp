#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str {"Hello World! Hello Kitty! Hello Baby!"};
    string target = "Hello";

    unsigned int count = 0, pos = 0;
    string::size_type i;

    while((i=str.find(target,pos)) != string::npos) {
        count++;
        cout << "(" << count << ") " << i << endl;
        pos = (unsigned int) i + 1;
    }
    cout << "NPOS: " << string::npos << endl;
}




