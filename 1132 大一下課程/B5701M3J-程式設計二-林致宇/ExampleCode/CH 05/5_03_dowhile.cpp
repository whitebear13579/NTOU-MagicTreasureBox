#include<iostream>
using namespace std;

int main() {

    int answer = 7, guess = -1;

    do {
        cout << "Please enter a number: ";
        cin >> guess;
    } while (guess != answer);
    cout << "Correct\n";
}
