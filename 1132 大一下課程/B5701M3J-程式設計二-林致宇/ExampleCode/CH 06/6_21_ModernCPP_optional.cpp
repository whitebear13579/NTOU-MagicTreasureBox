#include<iostream>
#include<optional>

using namespace std;

optional<int> f(int x) {
    if (x >= 0) {
        return x;
    } else {
        return std::nullopt;
    }
}

int main() {
    auto result1 { f(3)};
    auto result2 { f(-2)};

    if (result1 != std::nullopt) {
        cout << "Result 1\n";
    }
    if (result2 != std::nullopt) {
        cout << "Result 2\n";
    }
}






