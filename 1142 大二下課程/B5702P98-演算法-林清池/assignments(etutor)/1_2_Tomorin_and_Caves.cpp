// Category: Divide and Conquer

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

int crossMid( vi &v, int l, int m, int r ){
    int lsum = LLONG_MIN, rsum = LLONG_MIN, now = 0;
    // from mid to l, find max suffix
    for ( int i = m ; i >=l ; i-- ){
        now += v[i];
        if ( now > lsum ) lsum = now;
    }
    now = 0;
    // from mid to r, find max prefix
    for ( int i = m+1 ; i <= r ; i++ ){
        now += v[i];
        if ( now > rsum ) rsum = now;
    }
    return lsum+rsum;
}

int maxSubArray( vi &v, int l, int r ){
    if ( l == r ){
        return v[l];
    }
    int m = (l+r)>>1, lmax = 0, rmax = 0, crossmax = 0;
    lmax = maxSubArray( v, l, m );
    rmax = maxSubArray( v, m+1, r );
    crossmax = crossMid( v, l, m, r );
    return max( max( lmax, rmax ), crossmax );
}

inline signed solve(){
    int n = 0;
    n = nextint();
    vi stone(n, 0);
    for ( int i = 0 ; i < n ; i++ ) stone[i] = nextint();
    cout << maxSubArray( stone, 0, n-1 ) << "\n";
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