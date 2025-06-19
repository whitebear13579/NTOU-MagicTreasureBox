#include <iostream>
#include <memory>
#include <vector>
#include <sstream>
using namespace std;

/*
    this variable wiil control the test dump messages. (for local.)
    if false, the program will follow the problem's required output normaly at standard output.
    if true, the additional dump messages will output at standard error.
*/
const bool testDump = 0;
const int MAX = 1e9;

// 這份作業我是自己完成，而不是經由大型語言模型所產生

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

bool isSorted(){
    bool flag = 1;
    auto now = head;
    while ( now->next != nullptr ){
        if ( now->getNumber() > now->next->getNumber() ){
            flag = 0;
            break;
        }
        now = now->next;
    }
    return flag;
}

void swpNode( shared_ptr<Number> a , shared_ptr<Number> b ){
    //處理 A 的前一項 
    if ( a->previous != nullptr ) a->previous->next = b;
    b->previous = a->previous;
    //處理 B 的後一項
    a->next = b->next;
    if ( b->next != nullptr ) b->next->previous = a;

    //A和B之間
    a->previous = b;
    b->next = a;

    //如果需要 更新全域 head 和 tail;
    if ( a->previous == nullptr ) head = a;
    else if ( b->previous == nullptr ) head = b;

    if ( a->next == nullptr ) tail = a;
    else if ( b->next == nullptr ) tail = b;
    
    if ( testDump ){
        auto now = head;
        cerr << "Swap: ";
        while( now->next != nullptr ){
            cout << now->getNumber() << " ";
            now = now->next;
        } cout << now->getNumber() << "\n";
    }
}

void sortDLL() {
    //bubble sort
    while ( !isSorted() ){
        auto cmp = head;
        while( cmp->next != nullptr ){
            if ( cmp->getNumber() > cmp->next->getNumber() ){
                swpNode(cmp,cmp->next);
            }
            if ( cmp->next ) cmp = cmp->next;
        }
        if ( testDump ) cerr << "-------------------------------------\n";
    }
}  
  
void printDLLNumber() { 
    auto now = head;
    while( now->next != nullptr ){
        cout << now->getNumber() << " ";
        now = now->next;
    } cout << now->getNumber() << "\n";
}  
  
void readInput(vector<int> & v) {  
    string line, token;  
  
    getline(cin, line);  
    stringstream ss(line);  
  
    int count = 0;  
    while (getline(ss, token, ' ')) {  
        v.push_back(stoi(token));  
    }

    if ( testDump ){
        for ( auto i : v ) cerr << i << " ";
        cerr << "\n";
    }
}  

void makeDLL(vector<int> v) {
    shared_ptr<Number> element;
    for ( int i = 0 ; i < v.size() ; i++ ){
        element = make_shared<Number> (v[i]);
        if ( !i ){ //hasn't initialize head & tail.
            element->next = nullptr, element->previous = nullptr;
            head = element, tail = element;
        }else{ 
            element->next = nullptr;
            element->previous = tail;
            tail->next = element;
            tail = element;
        }

        /*if ( testDump ){
            cerr << "#Now Head "  << head->getNumber() << " | Next : " << head->next << "\n";
            cerr << "#Now Element "  << element->getNumber() << " | Previous : " << element->previous << "\n";
            cerr << "#Now Tail "  << tail->getNumber() << " | Previous : " << element->previous << "\n";
            cerr << "---------------------------------------------------------------------------\n";
        }*/
        
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