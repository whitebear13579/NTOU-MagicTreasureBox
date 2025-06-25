#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    enum {red = 5, blue, yellow} color = blue;
    cout << color << '\n';
    return 0;
}
