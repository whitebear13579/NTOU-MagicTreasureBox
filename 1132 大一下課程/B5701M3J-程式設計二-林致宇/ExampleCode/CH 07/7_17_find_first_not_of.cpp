#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str = "(02)-1234-5678";
    string target = "0123456789";
    
    unsigned int count = 0, pos = 0;
    string::size_type i;

    while((i=str.find_first_not_of(target,pos)) != string::npos) {
        count++;
        cout << "(" << count << ") " << i << endl;
        pos = (unsigned int) i + 1;
    }
}




