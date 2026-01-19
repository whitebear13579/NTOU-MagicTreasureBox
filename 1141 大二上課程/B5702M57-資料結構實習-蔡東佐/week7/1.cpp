#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define isp first
#define icp second
using namespace std;

map<char,pii> reflect = {
    {'(', { 0, 20}},
    {')', {19, 0}},
    {'+', {12, 12}},
    {'-', {12, 12}},
    {'*', {13, 13}},
    {'/', {13, 13}},
    {' ', { 0,  0}},
};

inline bool outputOper( char x ){
    bool ok = 0;
    switch (x) {
        case '(':
            break;
        
        case ')':
            break;

        default:
            ok = 1;
            cout << x;
            break;
    }
    return ok;
}

inline signed solve(){
    stack<char> oper;
    string str;
    bool firstOut = 1;

    getline(cin, str);
    //str = "( 1 + 3 ) * 2";
    
    int strl = str.length();
    for ( int i = 0 ; i < strl ; i++ ){
        auto x = str[i];

        if ( x == ' ' ) continue;

        if ( isdigit(x) ){
            if ( !firstOut ) cout << " ";
            cout << x;
            firstOut = 0;
        } else if ( x == '(' ){
            oper.push(x);
        } else if ( x == ')' ){
            while ( !oper.empty() and oper.top() != '(' ){
                if ( !firstOut ) cout << " ";
                outputOper(oper.top());
                firstOut = 0;
                oper.pop();
            }
            if ( !oper.empty() ) oper.pop();
        } else {
            while ( !oper.empty() and oper.top() != '(' and reflect[oper.top()].isp >= reflect[x].icp ){
                if ( !firstOut ) cout << " ";
                outputOper(oper.top());
                firstOut = 0;
                oper.pop();
            }
            oper.push(x);
        }
    }

    while ( !oper.empty() ){
        if ( !firstOut ) cout << " ";
        outputOper(oper.top());
        firstOut = 0;
        oper.pop();
    }
    cout << "\n";
    return 0;
}

signed main(){
    whitebear;
    int t = 0;
    cin >> t;
    cin.ignore();
    while(t--)
        solve();
    return 0;   
}