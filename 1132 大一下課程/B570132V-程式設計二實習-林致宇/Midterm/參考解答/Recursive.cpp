#include<iostream>    
#include<sstream>
#include <string>    
#include<vector>
using namespace std;

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

void reverse(vector<int> & v) {
    // 0 1 2 3 4 5 6
    // 6 5 4 3 2 1 0
    
    // 0 1 2 3 4 5
    // 5 4 3 2 1 0
    int half = v.size()/2; // 7/2 = 3 6/2 = 3
    for (int i = 0; i < half; i++) {
        int tmp = v[i];
        v[i] = v[v.size()-i-1];
        v[v.size()-i-1] = tmp;
    }
}

int findMax(vector<int> v) {
    
    if (v.size() == 1) {
        return v[0];
    }
    int last = v[v.size()-1];
    v.pop_back();
    int remainMax = findMax(v);
    if (last > remainMax) {
        return last;
    }
    return remainMax;
}



int main() {
   vector<int> v;
   readInput(v);
   reverse(v);
   printVector(v);
   int max = findMax(v);
   cout << max << endl;
}