/*ds06*/
#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

signed main(){
    whitebear;
    int n = 0;
    cin >> n;
    bool flag = 0;
    string input = "";
    for(int i = 0;i < n;i++){
        cin >> input;
        if ( input == "c8763" ) continue;
        stack<bool> checking;
        for(char k : input){
            if(k == '(') checking.push(1);
            else if(k == ')'){
                if(checking.empty()){
                    flag = 1;
                    break;
                }else checking.pop();
            }
        }
        if(flag or !checking.empty()) cout << "N\n";
        else cout << "Y\n";
        input = "";
        flag = 0; //most important QQ
    }
    return 0;
}