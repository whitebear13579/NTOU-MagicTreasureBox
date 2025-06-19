#include<iostream>
#include<sstream>  
#include<string>  
#include<vector>  
#include<memory>  
using namespace std;  
/*
    https://etutor2.itsa.org.tw/mod/teachingzone/problem_view.php?id=36653
    Problem Statement:
    Rotation of Doubly Linked List (30%)
    Given a doubly-linked list and an integer n, the task is to rotate the linked list clockwise or counter-clockwise by n nodes.

    1st line: n
    2nd line: 0 (clockwise) or 1 (counter-clockwise)
    3rd line: the list (consists of 10 numbers)

    Example:
    Input 1:
    3
    0
    32 78 26 19 58 27 9 68 70 92
    Output 1:
    68 70 92 32 78 26 19 58 27 9

    Input 2:
    2
    1
    32 78 26 19 58 27 9 68 70 92
    Output 2:
    26 19 58 27 9 68 70 92 32 78
    
    Caution: You should follow the following template (The only thing that you need to do is implementing roate() function.): ...
*/ 
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

/*
    0 後面N個接去前面
    1 前面N個接去後面
*/
void rotate(int n, int clockwise) {
    if (clockwise){ // 1
        while (n--){
            tail->next = head;
            tail->next->previous = tail;
            tail = tail->next;

            head = head->next;
            head->previous = nullptr;
            tail->next = nullptr;
        }
    }else{ //0
        while (n--){
            head->previous = tail;
            head->previous->next = head;
            tail = tail->previous;
            head->previous->previous = nullptr;
            head = head->previous;

            tail->next = nullptr;
        }
    }
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