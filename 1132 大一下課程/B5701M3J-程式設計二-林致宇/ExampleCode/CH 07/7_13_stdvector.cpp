#include<iostream>
#include<vector>
using namespace std;

int main(int argc, const char * argv[]) {

    vector<int> iarray {1, 6, 8};

    cout << "Size: " << iarray.size() << endl;

    iarray.push_back(5);

    for (int x: iarray) {
        cout << x << " ";
    }
    cout << endl;

    iarray.pop_back();

    for (int x: iarray) {
        cout << x << " ";
    }
    cout << endl;
}


