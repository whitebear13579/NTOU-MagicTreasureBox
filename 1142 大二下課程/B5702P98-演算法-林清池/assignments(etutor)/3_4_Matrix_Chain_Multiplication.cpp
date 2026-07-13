// Category: Dynamic Programming

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
    int n = 0, r = 0, c = 0;
    n = nextint();
    vi matrix(n+5);
    for ( int i = 0 ; i <= n ; i++ ){
        r = nextint(), c = nextint();
        if ( i == 0 ){
            matrix[i] = r;
            ++i;
            matrix[i] = c;
        }else matrix[i] = c;
    }
    vii dp(n+5,vi(n+5)), slice(n+5,vi(n+5));
    for ( int i = 0 ; i <= n ; i++ ) dp[i][i] = 0;
    for ( int l = 2 ; l <= n ; l++ ){
        for ( int i = 0 ; i <= n-l ; i++ ){
            int j = i + l - 1;
            dp[i][j] = LLONG_MAX;
            for ( int k = i ; k < j  ; k++ ){
                int now  = dp[i][k] + dp[k+1][j] + matrix[i] * matrix[k+1] * matrix[j+1];
                if ( now < dp[i][j] ){
                    dp[i][j] = now;
                    slice[i][j] = k;
                }
            }
        }
    }
    cout << dp[0][n-1] << "\n";
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