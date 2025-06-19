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
    vi uwu(10,0);
    int t = 0;
    for ( int i = 0 ; i < 10 ; i++ ){
        uwu[i] = nextint();

    }
    t = nextint();
    int ans = -1 ,l = 0 , r = 9;
    while ( l <= r ){
        int mid  = ( l + r ) >> 1;
        if ( uwu[mid] == t ){
            ans = mid;
            break;
        }else if ( uwu[mid] < t ){
            l = mid + 1;
        }else{
            r = mid - 1;
        }
    }
    cout << ans << "\n";
    return 0;
}

signed main(){
    whitebear;
    int t = 0;
    t = nextint();
    while(t--)
        solve();
    return 0;
}