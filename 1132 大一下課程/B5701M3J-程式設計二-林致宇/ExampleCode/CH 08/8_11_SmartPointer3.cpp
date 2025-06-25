#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    shared_ptr<int> x( new int {8} );
    shared_ptr<int> y = x;
    weak_ptr<int> z = y;

}



