#include<iostream>
#include<stdexcept>
#include <vector>
using namespace std;

int main() {
    std::vector<int> myvector(10);
    try {
        int n = myvector.at(20);
        cout << "OK!" << endl;
    } catch (const out_of_range &e) {
        cerr << e.what() << endl;
    }
}






