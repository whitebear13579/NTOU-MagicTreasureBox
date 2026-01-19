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
    int n = 0;
    n = nextint();
    vii table(n+5,vi(n+5,0));
    int r = 0, c = n/2 , rp = 0, cp = 0;
    
    table[r][c] = 1;
    for ( int i = 2 ; i <= n*n ; i++ ){
        rp = r, cp = c;
        --r, ++c;

        if ( r < 0 ) r = n-1;
        if ( c > n-1 ) c = 0;
        if ( table[r][c] ){
            r = rp+1, c = cp;
            if ( r > n-1 ) r = 0;
        }
        table[r][c] = i;
    }

    for ( int i = 0 ; i < n ; i++ ){
        for ( int j = 0 ; j < n ; j++ ){
            cout << table[i][j] << " ";
        }cout << "\n";
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