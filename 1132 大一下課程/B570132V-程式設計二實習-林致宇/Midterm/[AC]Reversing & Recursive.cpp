#include<iostream>    
#include<sstream>
#include <string>    
#include<vector>
using namespace std;

/*
    https://etutor2.itsa.org.tw/mod/teachingzone/problem_view.php?id=36652
    Problem Statement:
    Reversing & Recursive (30%)
    Given a vector std::vector<int> v, you should (1) read inputs and store the numbers into the v, (2) reversing the elements of the v, (3) output the reversed vector, and (4) find the maximum element in v recursively. (Hint: You may need to call pop_back() in the findMax function.)
 
    Your main program should follow the following format (If you don't follow the template, you will only get partial credite.):...
    You should implement reverse() and findMax(). Note that findMax should be a recursive function. In addition, both reverse() and findMax() only have one parameter.
 
    You can copy the codes of readInput() and printVector() to your code directly.
    void readInput(vector<int> & v) {    
        ...   
    }

    void printVector(vector<int> v) {    
        ...   
    }
    * Since STL is used, please remember to select C++11 for the compiler.

    Example:
    Input 1:
    32 27 96 18 35 52
    Output 1:
    52 35 18 96 27 32
    96

    Input 2:
    72 83 26
    output 2:
    26 83 72
    83
*/

/*
Given a vector std::vector<int> v, you should 
(1) read inputs and store the numbers into the v, 
(2) reversing the elements of the v, 
(3) output the reversed vector, and 
(4) find the maximum element in v recursively. 
(Hint: You may need to call pop_back() in the findMax function.)
 
Your main program should follow the following format (If you don't follow the template, you will only get partial credite.):
*/

void readInput(vector<int> & v) {    
   string line, token;    
   getline(cin, line);    
   stringstream ss(line);    
   int count = 0;    
   while (getline(ss, token, ' ')) {    
       v.push_back(stoi(token));    
   }    
}

void printVector(vector<int> v) {    
   for (int i = 0; i < v.size(); i++) {    
       cout << v[i];    
       if (i == v.size()-1) {    
           cout << endl;    
       } else {    
           cout << " ";    
       }    
   }    
}

void reverse( vector<int> &v ){
    vector<int> temp;
    for ( int i = v.size()-1 ; i >= 0 ; i-- ){
        temp.push_back(v[i]);
    }
    v = temp;
}

int findMax ( const vector<int> &v ){
    if ( v.size() == 1 ){
        return v[0];
    }
    vector<int> temp;
    temp = v;
    if ( temp[temp.size()-1] < temp[temp.size()-2] ){ // 假設倒數第二個比倒數第一的大
        temp.pop_back();
    }else{
        // 假設倒數第一個比倒數第二的大
        swap(temp[temp.size()-1],temp[temp.size()-2]);
    }
    return findMax(temp);
}

int main() {
   vector<int> v;
   readInput(v);
   reverse(v);
   printVector(v);
   int min = findMax(v); // 對，不要懷疑，老師的範本程式碼真的把 findmax 的回傳值叫 min 
   cout << min << endl;
}