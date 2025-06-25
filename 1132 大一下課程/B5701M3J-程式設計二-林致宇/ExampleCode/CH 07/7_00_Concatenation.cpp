#include<iostream>
#include<string>
using namespace std;

int main(int argc, char * argv[]) {

    string str1 {"Hello " "World!"};
    int numberOfBooks = 5;
    
    cout << str1 << endl;
    cout << ("There are " + to_string(numberOfBooks) +
        " books on the desk.") << endl;
}


