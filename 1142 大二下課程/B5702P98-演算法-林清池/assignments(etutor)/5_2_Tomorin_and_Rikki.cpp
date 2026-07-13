// Category: Graph

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define Y "YES\n"
#define N "NO\n"
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

bool dfs( int st, int ed, vector<bool> &vis, vii &g ){
    if ( st == ed ) return 1;
    vis[st] = 1;
    for ( auto ng : g[st] ){
        if ( !vis[ng] ){
            if ( dfs(ng,ed,vis,g) ){
                return 1;
            }
        }
    }
    return 0;
}

inline signed solve(){
    int n = 0, m = 0, u = 0, v = 0;
    n = nextint(), m = nextint();
    vii g( n+1 );
    vector<bool> vis(n+1);
    for ( int i = 0 ; i < m ; i++ ){
        u = nextint();
        g[u].push_back(nextint());
    }
    u = nextint(), v = nextint();
    bool can_uv = dfs(u,v,vis,g);
    for ( int i = 0 ; i <= n ; i++  ) vis[i] = 0;
    bool can_vu = dfs(v,u,vis,g);
    cout << (  can_uv and can_vu ? Y : N  );
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