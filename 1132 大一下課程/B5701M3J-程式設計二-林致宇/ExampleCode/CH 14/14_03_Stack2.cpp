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

class Car {
public:
    int id;
};

int main() {
    Car car1; car1.id = 1;
    Car car2; car2.id = 2;
    Car car3; car3.id = 3;

    Stack<Car,2> stack1;
    cout << stack1.push(car1) << endl;
    cout << stack1.push(car2) << endl;
    cout << stack1.push(car3) << endl;
    do {
        try {
            Car result = stack1.pop();
            cout << result.id << endl;
        } catch (int e) {
            cout << "Empty!" << endl;
            break;
        }
    } while (true);
}











