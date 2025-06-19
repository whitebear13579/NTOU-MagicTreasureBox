#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define int long long 
using namespace std;
 
//usage cin >> n -> n = nextint()
 
inline char readchar() {
    const int S = 1<<20; // buffer size
    static char buf[S], *p = buf, *q = buf;
    if(p == q && (q = (p=buf)+fread(buf,1,S,stdin)) == buf) return EOF;
    return *p++;
}
 
inline int nextint() {
    int x = 0, c = getchar(), neg = false;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = true, c = getchar();
    while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

inline signed solve(){
    vector<pii> coins = {{1000,0}, {500,0}, {200,0}, {100,0}, {50,0}, {10,0}, {5,0}, {1,0}};
    int n = 0;
    n = nextint();
    int ptr = 0;
    while( n != 0 || ptr < 8 ){
        if ( n >= coins[ptr].f ){
            n -= coins[ptr].f;
            coins[ptr].s++;
        }else ptr++;
    }
    for ( auto i : coins ) cout << i.s << ( i.f == 1 ? "\n" : " " );
    return 0;
}

signed main(){
    whitebear;
    int t = 0;
    t = nextint();
    while(t--)
        solve();
    return 0;
}