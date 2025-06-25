#include<iostream>
using namespace std;

int main() {

    int answer = 7, guess = -1;

    while (guess != answer) {
        cout << "Please enter a number: ";
        cin >> guess;
    }
    cout << "Correct\n";
}
