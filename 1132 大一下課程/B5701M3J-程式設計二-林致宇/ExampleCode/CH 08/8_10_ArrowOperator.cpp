#include<iostream>
#include<vector>
using namespace std;

int main(int argc, char * argv[]) {

    vector<int> iarr;
    vector<int> *piarr = &iarr;

    (*piarr).push_back(1);
    piarr->push_back(6);

    for (int x: iarr) {
        cout << x << endl;
    }
}


