// Category: Graph

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define Y "Yappeee\n"
#define N "Bruh\n"
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

bool bfs( vii &g, int n ){
    vi c(n+1,0);
    queue<int> q;
    for ( int i = 1 ; i <= n ; i++ ){
        if ( !c[i] ){
            c[i] = 1;
            q.emplace(i);
            while ( !q.empty() ){
                int now = q.front();
                q.pop();
                for ( auto nt : g[now] ){
                    if ( !c[nt] ){
                        if ( c[now] == 1 ) c[nt] = 2;
                        else if ( c[now] == 2 ) c[nt] = 1;
                        q.emplace(nt);
                    }else if ( c[nt] == c[now] ){
                        return 0;
                    }
                }
            }
        }
    }
    return 1;
}

inline signed solve(){
    int n = 0, m = 0, u = 0, v = 0;
    n = nextint(), m = nextint();
    vii ban(n+1);
    for ( int i = 0 ; i < m ; i++ ){
        u = nextint(), v = nextint();
        ban[u].push_back(v);
        ban[v].push_back(u);
    }
    cout << ( bfs(ban, n ) ? Y : N ); 
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