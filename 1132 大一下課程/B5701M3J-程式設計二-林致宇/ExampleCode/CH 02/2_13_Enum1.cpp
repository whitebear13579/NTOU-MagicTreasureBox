#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    enum fruit { apple, banana, orange };
    fruit f1 = apple;
    fruit f2 = orange;

    cout << f1 << ", " << f2 << '\n';

    return 0;
}
