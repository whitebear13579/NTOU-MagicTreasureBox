#include<iostream>
#include<vector>
using namespace std;

int main(int argc, char * argv[]) {

    vector<int> v;
    vector<int>::iterator it;
    
    for (int i = 0; i<10 ; i++) {
        v.push_back(i);
    }

    for(it = v.begin(); it !=v.end(); it++) {
        cout << *it;
    }
    cout << endl << *(it-1) << endl;;

}



