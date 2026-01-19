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

/*
    - 走過的路徑標示為G
    - 在還有其它路可走的情況下，不能走標示G的路徑
    - 如發現無路可走時，退回上一步，並將此格標示為D，表示不得再走此格。
    - 出發點標示為S
    - 出口標示為X
*/

int dx[4] = { 0, -1,  0, 1 };
int dy[4] = { 1,  0, -1, 0 };

struct Node{
    int x, y, d;
};

inline signed solve(){
    int endR = 1, endC = 1;
    bool ok = 0;

    vector<vector<bool>> maze(15,vector<bool>(15));
    vector<vector<char>> visted(15,vector<char>(15));

    for ( int i = 0 ; i < 10 ; i++ ){
        for ( int j = 0 ; j < 10 ; j++ ){
            maze[i][j] = nextint();
            maze[i][j] ? visted[i][j] = '1' : visted[i][j] = '0';
        }
    }

    stack<Node> path;
    path.push({8, 8 ,0});
    visted[8][8] = 'S';

    while ( !path.empty() and !ok ){
        auto [nowR, nowC, nowD] = path.top();
        
        if ( nowD >= 4 ){
            if ( visted[nowR][nowC] != 'S' ) visted[nowR][nowC] = 'D';
            path.pop();
            continue;
        }else path.top().d++;

        int nextR = nowR + dx[nowD], nextC = nowC + dy[nowD];

        if ( nextR < 0  or nextR >= 10 or nextC < 0 or nextC >= 10 ){
            /* edge case */
            continue;
        }

        if ( nextR == endR and nextC == endC ){
            ok = 1;
            visted[nextR][nextC] = 'X';
            break;
        }

        if ( maze[nextR][nextC] == 0 and visted[nextR][nextC] == '0' ){
            visted[nextR][nextC] = 'G';
            path.push({nextR, nextC, 0});
        }
    }
    
    if (ok) visted[endR][endC] = 'X';

    cout << (ok ? "YES\n" : "NO\n");
    for ( int i = 0 ; i < 10 ; i++ ){
        for ( int j = 0 ; j < 10 ; j++ )
        {
            cout << visted[i][j] << (j == 9 ? "" : " ");
        } cout << "\n";
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