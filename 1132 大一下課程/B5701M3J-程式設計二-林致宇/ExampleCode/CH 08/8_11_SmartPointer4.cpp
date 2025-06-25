#include<iostream>
using namespace std;

int main(int argc, char * argv[]) {

    shared_ptr<int> x = nullptr;
    x = make_shared<int>(8);
    cout << *x << endl;

}



