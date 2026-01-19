#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

inline signed solve(){
    string str, repl , tar;
    getline(cin, str);
    getline(cin, repl);
    getline(cin, tar);
    size_t pos = 0;
    while ( (pos = str.find(repl, pos)) != string::npos ){
        str.replace(pos, repl.length(), tar);
        pos += tar.length();
    }
    cout << str << "\n";
    return 0;
}

signed main(){
    whitebear;
    /*int t = 0;
     t = nextint();
     while(t--)*/
         solve();
    return 0;
}