#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    int *pi = new int {5};
    cout << *pi << '\n';
    delete pi;
    // pi = nullptr;

    if (pi != nullptr)
        cout << *pi << endl;
}



