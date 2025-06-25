#include<iostream>
#include <set>
using namespace std;

int main() {
    std::set<int> myset = {3, 1};
    myset.insert(2);
    myset.insert(6);
    myset.insert(5);
    myset.insert(3);
    myset.insert(5);

    for (auto s : myset) {
        std::cout << s << " ";
    }
    std::cout << endl;
}



