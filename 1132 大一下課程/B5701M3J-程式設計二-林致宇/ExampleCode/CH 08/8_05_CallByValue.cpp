#include<iostream>
using namespace std;

void addOne(int x) {
    x++;
}

int main(int argc, const char * argv[]) {

    int x = 5;
    addOne(x);
    cout << x << '\n';    
}


