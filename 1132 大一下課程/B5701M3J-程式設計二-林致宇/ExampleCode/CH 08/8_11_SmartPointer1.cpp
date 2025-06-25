#include<iostream>
#include<vector>
using namespace std;

int main(int argc, char * argv[]) {

    unique_ptr<int> x( new int {8} );
    cout << *x << endl;

    unique_ptr< vector<int> > y( new vector<int>() );
    y->push_back(6);
    cout << y->at(0) << endl;
}


