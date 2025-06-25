#include<iostream>
#include <vector>
using namespace std;

template<typename T>
bool isEqual(T data1, T data2) {
    if (data1 == data2) {
        return true;
    }
    return false;
}
template<>
bool isEqual<char *>(char * data1, char * data2) {
    if (strcmp(data1, data2) == 0 ) {
        return true;
    }
    return false;
}

int main() {
    cout << boolalpha;
    char * str1 = "Hello";
    char * str2 = "Hello";
    cout << isEqual(str1, str2) << endl;
}











