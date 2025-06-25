#include<iostream>
#include <queue>
using namespace std;

int main() {
    queue<int> q;
    q.push(1);
    q.push(2);
    q.push(3);

    cout << q.front() << endl;
    cout << q.back() << endl;

    /* This for loop has a problem! */
    for (int i = 0; i < q.size(); i++) {
        cout << q.front() << ' ';
        q.pop();
    } // Only 1 and 2 will be printed.
    cout << endl;
}


