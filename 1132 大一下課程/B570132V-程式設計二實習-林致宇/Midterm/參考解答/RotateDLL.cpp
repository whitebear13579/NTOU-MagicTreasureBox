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

void rotate(int n, int clockwise) {
    
    // 1 2 3 4 5 6 7 8 9 10
    
    
    n = n % 10;
    shared_ptr<Number> newhead = nullptr;
    if (clockwise == 0) {
        
        // Clock-wise
        // 8 9 10 1 2 3 4 5 6 7
        
        newhead = tail;
        for (int i = 1; i < n; i++) {
            newhead = newhead->previous;
        }
        
    } else {
    
        // Counter clock-wise
        // 3 4 5 6 7 8 9 10 1 2
        newhead = head;
        for (int i = 0; i < n; i++) {
            newhead = newhead->next;
        }
    }
    shared_ptr<Number> newtail = newhead->previous;
    
    tail->next = head;
    head->previous = tail;
        
    newhead->previous = nullptr;
    newtail->next = nullptr;
        
    head = newhead;
    tail = newtail;

}  
 
void print() {  
 
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
 
void make(vector<int> v) {  
 
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
  
   int n;
   int clockwise;
   cin >> n;
   cin >> clockwise;
   vector<int> v;

   for (int i = 0; i < 10; i++) {
       int number;
       cin >> number;
       v.push_back(number);
   }
   make(v);
   rotate(n, clockwise);
   print();
}