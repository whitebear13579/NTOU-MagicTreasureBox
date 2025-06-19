#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define int unsigned long long
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

vi fib(1000,0);

inline signed solve( int n ){
    if ( fib[n] or n == 0 ) return fib[n];
    else{
        fib[n] = solve(n-1) + solve(n-2);
        return fib[n];
    }
}

signed main(){
    whitebear;
    int n = 0;
    n = nextint();
    fib[0] = 0, fib[1] = 1;
    for ( int i = 0 ; i < n ; i++ ){
        cout << solve(i) << (i == n-1 ? "\n" : " ");
    }
    return 0;
}