#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str {"(1) Hello World! (2) Hello Kitty! (3) Hello Baby!"};
    string target = "0123456789";
    
    unsigned int count = 0, pos = 0;
    string::size_type i;

    while((i=str.find_first_of(target,pos)) != string::npos) {
        count++;
        cout << "(" << count << ") " << i << endl;
        pos = (unsigned int) i + 1;
    }
}




