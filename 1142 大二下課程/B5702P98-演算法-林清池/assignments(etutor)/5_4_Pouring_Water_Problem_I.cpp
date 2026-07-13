// Category: Graph

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define Y "True\n"
#define N "False\n"
using namespace std;
 
//usage cin >> n -> n = nextint()

struct iiyo_koiyo_114514{
    int x;
    int y;
    int ops;
};
 
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

inline bool solve( int x, int y, int t, int k ){
    if ( t > x + y ) return 0;
    queue<iiyo_koiyo_114514> q;
    set<pii> vis;
    q.push({ 0, 0, 0 });
    vis.insert({ 0, 0 });
    while ( !q.empty() ){
        auto [a, b, op] = q.front();
        q.pop();
        if ( a + b == t ) return 1;
        if ( op >= k ) continue;
        int py = min(a, y-b), px = min(b, x-a);
        vector<pii> nxt = {
            {x, b},
            {a, y},
            {0, b},
            {a, 0},
            {a - py, b + py},
            {a + px, b - px}
        };
        
        for ( auto i : nxt ){
            if ( vis.count(i) == 0  ){
                vis.insert(i);
                q.push({i.f, i.s, op+1});
            }
        }
    }
    return 0;
}

signed main(){
    whitebear;
    int x = 0, y = 0, t = 0, k = 0;
    x = nextint(), y = nextint(), t = nextint(), k = nextint();
    cout << ( solve(x,y,t,k) ? Y : N);
    return 0;
}