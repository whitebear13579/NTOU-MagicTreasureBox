// Category: Dynamic Programming

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define int long long
#define NO "RE: START :<"
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
    int n = 0, m = 0, k = 0;
    n = nextint(), m = nextint(), k = nextint();
    vii boss(n,vi(m)), dp(n,vi(m));
    for ( int i = 0 ; i < n ; i++ ) for ( int j = 0 ; j < m ; j++ ) boss[i][j] = nextint();
    for ( int i = 0 ; i < n ; i++ ) for ( int j = 0 ; j < m ; j++ ) dp[i][j] = LLONG_MAX;
    for ( int i = 0 ; i < n ; i++ ) dp[i][0] = boss[i][0];
    for ( int j = 1 ; j < m ; j++ ){
        for ( int i = 0 ; i < n ; i++ ){
            int u = (i-1+n)%n, m = i, d = (i+1)%n, prev = 0;
            prev = min(dp[u][j-1], min(dp[m][j-1], dp[d][j-1]));
            dp[i][j] = prev + boss[i][j];
        }
    }
    int ans = LLONG_MAX;
    for ( int i = 0 ; i < n ; i++ ){
        if ( dp[i][m-1] < ans ) ans = dp[i][m-1];
    }
    if ( ans <= k ) cout << ans;
    else cout << NO;
    cout << "\n";
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