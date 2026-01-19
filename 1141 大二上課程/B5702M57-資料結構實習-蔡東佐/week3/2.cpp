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
    vector<pii> num(n);

    for ( int i = 0 ; i < n ; i++ ) num[i].f = nextint() , num[i].s = i;
    sort(num.begin(), num.end(), greater<pii>());

    int ans = LLONG_MIN;
    
    for ( int i = 0 ;  i < min(n, 8LL) ; i++ ){
        for ( int j = i + 1 ; j < min(n, 8LL) ; j++ ){
            if ( abs(num[i].s - num[j].s) > 1 ) ans = max(ans, num[i].f + num[j].f);
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