#include<iostream>
#include<limits>
using namespace std;

int main(int argc, const char * args[]) {

    cout << "The range for type unsigned long is from "
        << numeric_limits<unsigned long>::min() << " to "
        << numeric_limits<unsigned long>::max() << endl;        
    return 0;
}

