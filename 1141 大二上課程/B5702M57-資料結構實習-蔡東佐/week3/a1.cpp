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
    int n = 0;
    n = nextint();
    vi blocks;
    
    while(1){
        int t = 0;
        t = nextint();
        if ( t == 0 ) break;
        else blocks.push_back(t);
    }

    if ( blocks.empty() ) {
        for ( int i = 0 ; i < n ; i++ ) {
            cout << ( i == 0 ? "" : " " ) << 0;
        }cout << "\n";
    } else {
        vector<int> l(blocks.size(),0), r(blocks.size(),0), ans(n,0);
        int ptr = 0;
        
        for ( int i = 0 ; i < blocks.size() ; i++ ) {
            l[i] = ptr;
            ptr += blocks[i] + 1;
        }
        
        ptr = n;
        for ( int i = blocks.size() - 1 ; i >= 0 ; i-- ) {
            ptr -= blocks[i];
            r[i] = ptr;
            ptr--;
        }

        for ( int i = 0 ; i < blocks.size() ; i++ ) {
            int le = l[i] + blocks[i] - 1 , rb = r[i];
            if ( rb <= le ){
                for ( int p = max(0LL,rb) ; p <= min(n-1,le) ; p++ ){
                    ans[p] = 1;
                }
            }
        }
        
        for ( int i = 0 ; i < n ; i++ ) cout << ( i == 0 ? "" : " " ) << ans[i];
        cout << "\n";
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