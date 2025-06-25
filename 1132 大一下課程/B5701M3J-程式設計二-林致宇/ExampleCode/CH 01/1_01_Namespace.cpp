#include<iostream> 
using namespace std;

namespace myspace {
    int fun(int i) {
        return i * i;
    }
}

int main() {

    cout <<  myspace::fun(5) << "\n";
    return 0;
}


