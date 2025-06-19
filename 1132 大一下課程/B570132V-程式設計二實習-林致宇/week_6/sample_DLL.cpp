#include<iostream>  
#include<sstream>    
#include<string>    
#include<vector>    
#include<memory>    
using namespace std;    
  
class Number {  
private:    
    int number;    
public:    
    Number(int n) { number = n;}    
    int getNumber();    
    shared_ptr<Number> next;    
    shared_ptr<Number> previous;    
};    
    
int Number::getNumber() {    
    return number;    
}    
    
shared_ptr<Number> head;    
shared_ptr<Number> tail;    
    
shared_ptr<Number> findMinFromTheWall(shared_ptr<Number> wall) {    
    
    int minValue = wall->getNumber();    
    shared_ptr<Number> min = wall;    
    
    shared_ptr<Number> checkedItem = wall->next;    
    
    while(checkedItem != nullptr) {    
        if (checkedItem->getNumber() < minValue) {    
            minValue = checkedItem->getNumber();    
            min = checkedItem;    
        }    
        checkedItem = checkedItem->next;    
    }    
    return min;    
}  
    
void sortDLL() {    
    
    shared_ptr<Number> wall = head;    
    
    do {    
        if (wall == tail) {    
            break;    
        }    
    
        shared_ptr<Number> min = findMinFromTheWall(wall);    
    
        if (min == wall) {    
            wall = wall->next;    
            continue;    
        }    
    
        min->previous->next = min->next;    
        if(min->next != nullptr) {    
            min->next->previous = min->previous;    
        } else {    
            tail = min->previous;    
        }    
    
        min->next = wall;    
        min->previous = wall->previous;    
        if (min->previous == nullptr) {    
            head = min;    
        } else {    
            min->previous->next = min;                
        }    
        wall->previous = min;    
    } while (wall != nullptr);    
}    
    
void printDLLNumber() {    
    
    shared_ptr<Number> item = head;    
    do {    
        cout << item->getNumber();    
        if (item == tail) { // if (item->next == nullptr)    
            cout << endl;    
        } else {    
            cout << ' ';    
        }    
        item = item->next;    
    } while(item != nullptr);    
}    
    
void readInput(vector<int> & v) {    
    string line, token;    
    
    getline(cin, line);    
    stringstream ss(line);    
    
    int count = 0;    
    while (getline(ss, token, ' ')) {    
        v.push_back(stoi(token));    
    }    
}    
    
void makeDLL(vector<int> v) {    
    
    shared_ptr<Number> newitem = make_shared<Number>(v[0]);    
    
    newitem->previous = nullptr;    
    newitem->next = nullptr;    
    head = newitem;    
    tail = newitem;    
    
    for (int i = 1; i < v.size(); i++) {    
    
        newitem = make_shared<Number>(v[i]);    
        newitem->next = nullptr;    
        newitem->previous = tail;            
        tail = newitem;    
    
        newitem->previous->next = newitem;    
    }    
}    
    
int main(int argc, char * argv[]) {    
    
    vector<int> v;    
    readInput(v);    
    if (v.size() == 0) {    
        cout << endl;    
        return 0;    
    }    
    makeDLL(v);    
    sortDLL();    
    printDLLNumber();    
}