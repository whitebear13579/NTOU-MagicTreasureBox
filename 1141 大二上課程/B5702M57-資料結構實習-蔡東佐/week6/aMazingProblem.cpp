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

int dx[8] = {-1, -1, 0, 1, 1, 1, 0, -1};
int dy[8] = {0, 1, 1, 1, 0, -1, -1, -1};

struct Node{
    int x, y, d;
};

inline signed solve(){
    int n = 0, m = 0, endR = 0, endC = 0;
    bool ok = 0;
    n = nextint(), m = nextint();
    endR = n-1, endC = m-1;
    vector<vector<bool>> maze(n+5,vector<bool>(m+5)), visted(n+5,vector<bool>(m+5));
    for ( int i = 0 ; i < n ; i++ ){
        for ( int j = 0 ; j < m ; j++ ){
            maze[i][j] = nextint();
        }
    }

    stack<Node> path;
    path.push({0, 0 ,0});
    visted[0][0] = 1;

    while ( !path.empty() and !ok ){
        auto [nowR, nowC, nowD] = path.top();
        path.pop();

        for ( int t = nowD ; t < 8 ; ++t ){
            if ( ok )  break;
            int nextR = nowR + dx[t], nextC = nowC + dy[t];

            if ( nextR < 0  or nextR >= n or nextC < 0 or nextC >= m ){
                /* edge case */
                continue;
            }

            if ( nextR == endR and nextC == endC ){
                ok = 1;
                path.push({nowR, nowC, nowD});
                path.push({nextR, nowC, t});
                break;
            }

            if ( !maze[nextR][nextC] and !visted[nextR][nextC] ){
                visted[nextR][nextC] = 1;
                path.push({nowR, nowC, t+1});
                nowR = nextR, nowC = nextC, nowD = 0;
            }
        }
    }
    
    if ( ok ){
        cout << "The Path is:\nrow col\n";
        vector<pii> out;
        while( !path.empty() ){
            auto now = path.top();
            out.push_back({now.x, now.y});
            path.pop();
        }
        reverse(out.begin(), out.end());
        for ( auto it : out ){
            cout << it.f << " " << it.s << "\n";
        }
    } else {
        cout << "The maze does not have a path\n";
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