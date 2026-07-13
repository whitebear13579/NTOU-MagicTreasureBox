// Category: Dynamic Programming

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

inline signed solve(){
    int n = 0, w = 0;
    n = nextint(), w = nextint();
    vi stone(n), v(n);
    vii dp(n+5, vi(w+5)); // stones / weight
    for ( int i = 0 ; i <  n ; ++i ) stone[i] = nextint(), v[i] = nextint();
    for ( int i = 0 ; i < n ; ++i ){ // 第 0 ~ n-1 石頭
        for ( int j = 0 ; j <= w ; ++j ){ // 重量 0 ~ 重量 w
            if ( j - stone[i] < 0 ){
                // 塞不下新石頭
                dp[i+1][j] = dp[i][j];
            }else{
                // now = max(不拿或拿)
                dp[i+1][j] = max( dp[i][j], dp[i][j - stone[i]] + v[i] );
                // dp[i][j] -> 不拿，沿用上一狀態的價值
                // dp[i][j - stone[i]] + v[i] -> 拿，第二維重量先減掉去查表之前的最大值，加上現在價值
            }
        }
    }
    cout << dp[n][w] << "\n";
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