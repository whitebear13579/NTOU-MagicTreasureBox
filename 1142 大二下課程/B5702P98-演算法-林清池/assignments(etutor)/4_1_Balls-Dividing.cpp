// Category: Binary Search

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
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

bool limit( int op, int k, vi &ouo ){
    int total = 0;
    for ( auto i : ouo ){
        int now = (i-1)/op;
        total += now;
        if ( total > k ) return 0;
    }
    return 1;
}

inline signed solve(){
    int n = 0, k = 0, l = 1, r = 0;
    n = nextint();
    k = nextint();
    vi uwu(n,0);
    for ( int i = 0 ; i < n ; i++ ){
        uwu[i] = nextint();
        if ( uwu[i] > r ) r = uwu[i];
    }
    int ans = r;
    while ( l <= r ){
        int mid = (l+r)>>1;
        if ( limit( mid, k, uwu ) ){
            ans = mid;
            r = mid - 1;
        }else{
            l = mid + 1;
        }
    }
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