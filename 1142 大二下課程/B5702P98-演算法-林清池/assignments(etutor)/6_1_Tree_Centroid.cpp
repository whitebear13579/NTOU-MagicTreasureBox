// Category: Tree

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

const int N = 2e5;
vi s(N), w(N);

void dfs( int now, int par, vii &g, int n ){
    s[now] = 1;
    w[now] = 0;
    for ( auto v : g[now] ){
        if ( v != par ){
            dfs(v, now, g, n);
            s[now] += s[v];
            w[now] = max(w[now], s[v]);
        }
    }
    w[now] = max(w[now], n - s[now]);
}

inline signed solve(){
    int n = 0, u = 0, v = 0;
    n = nextint();
    vii tr(n+1);
    for ( int i = 0 ; i < n-1 ; i++ ){
        u = nextint(), v = nextint();
        tr[u].push_back(v);
        tr[v].push_back(u);
    }
    dfs(0, -1, tr, n);
    int uwu = INT_MAX, ans = 0;
    for ( int i = 0 ; i < N ; i++ ){
        if ( w[i] != 0 and w[i] < uwu ){
            uwu = w[i], ans = i;
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