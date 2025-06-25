#include<iostream>
#include <map>
using namespace std;

int main() {

    map<int, string> mapStudent;
    map<int, string>::iterator iter;

    mapStudent.insert(pair<int, string>(1, "Bob"));
    mapStudent[2] = "John";
    mapStudent.insert(pair<int, string>(3, "Bob"));
    mapStudent.insert(pair<int, string>(1, "Mary"));
    
    for(iter = mapStudent.begin(); iter != mapStudent.end(); iter++)
                cout<< iter->first << ' ' << iter->second<< endl;
}


