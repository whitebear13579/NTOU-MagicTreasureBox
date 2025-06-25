#include<iostream>
#include <vector>
using namespace std;

template<class T, int SIZE = 3>
class Stack {
private:
    int count;
    T buffer[SIZE];
public:
    Stack() { count = 0; };
    int push(T data);
    T pop();    
};

template<class T, int SIZE>
int Stack<T, SIZE>::push(T data) {
    if (count == SIZE) {
        return -1;
    } else {
        buffer[count++] = data;
    }
    return count;
}

template<class T, int SIZE>
T Stack<T, SIZE>::pop() {
    if (count == 0) {
        throw -1;
    }
    return buffer[--count];
}

int main() {
    Stack<int,2> stack1;
    cout << stack1.push(1) << endl;
    cout << stack1.push(2) << endl;
    cout << stack1.push(3) << endl;
    do {
        try {
            auto result = stack1.pop();
            cout << result << endl;
        } catch (int e) {
            cout << "Empty!" << endl;
            break;
        }
    } while (true);
}









