#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

int find ( const string &str, int l , int r ){
    int len = str.length();
    while ( l >= 0 and r < len and str[l] == str[r] ){
        --l, ++r;
    }
    return r-l-1;
}

inline signed solve(){
    string str;
    cin >> str;
    int st = 0, maxl = 1, len = str.length();
    for ( int i = 0 ; i < len - maxl/2 ; ++i ){
        int odds = find(str, i-1, i+1 );
        int even = find(str, i  , i+1 );

        int now = max(odds, even);
        if ( now > maxl ){
            maxl = now;
            st = i;
        }
    }
    auto ans = str.substr(st-(maxl-1)/2, maxl);
    cout << ans << "\n";
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