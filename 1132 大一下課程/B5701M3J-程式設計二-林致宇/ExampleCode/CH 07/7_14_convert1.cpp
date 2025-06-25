#include<iostream>
#include<string>
using namespace std;

int main(int argc, const char * argv[]) {

    char array[] = "Hello World";
    string sarray[] = {
        string(),
        string(array),
        string(array, 5),
        string(array, 6, 5),
        string(10, 'x')
    };
    for (int i = 0; i < 5; i++) {
        cout << sarray[i] << ", "
            << sarray[i].size() << ", "
            << sarray[i].length() << endl;
    }
}


