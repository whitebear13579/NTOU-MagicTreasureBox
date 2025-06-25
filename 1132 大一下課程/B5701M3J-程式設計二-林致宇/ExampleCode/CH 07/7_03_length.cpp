#include<iostream>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    char name[80];

    cout << "請輸入字串: ";

    cin.getline(name, 80);

    cout << "Length: " << strlen(name) << "\n";
}
