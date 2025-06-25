#include<iostream>
using namespace std;

int main(int argc, const char * args[]) {

    double d1 {8.6};
    double d2 {6.8};
    int i = {static_cast<int>(d1) + static_cast<int>(d2)};

    cout << d1 << " , " << d2 << " , " << i << std::endl;
    return 0;
}

