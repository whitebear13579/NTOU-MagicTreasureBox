#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    unique_ptr<int> x( new int {8} );
    unique_ptr<int> y = x;

}


