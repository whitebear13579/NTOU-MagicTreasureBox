// Category: Binary Search

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

inline int query ( int q, vi &range ){
    auto it = lower_bound(range.begin(), range.end(), q);
    int ans = (int)distance(range.begin(), it);
    return (ans+1);
}

inline signed solve(){
    int n = 0, m = 0, q = 0;
    n = nextint();
    vi stone(n,0), range(n,0);
    for ( int i = 0 ; i < n ; i++ ) stone[i] = nextint();
    range[0] = stone[0];
    for ( int i = 1 ; i < n ; i++ ) range[i] = range[i-1] + stone[i];
    m = nextint();
    while ( m-- ){
        q = nextint();
        cout << query( q, range ) << "\n";
    }
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