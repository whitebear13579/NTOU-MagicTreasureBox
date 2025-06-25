#include<iostream>
#include<stack>
using namespace std;

int main() {
    stack<int> s1;
    stack<double> s2;

    s1.push(5);
    s1.push(6);
    cout << s1.top() << endl;
    s1.pop();
    cout << s1.top() << endl;
    s1.pop();

    s2.push(3.5);
    cout << s2.top() << endl;
    s2.pop();
}












