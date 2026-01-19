#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define ENDL cout << "\n"
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
    vi number(n,0);
    for ( int i = 0 ; i < n ; i++ ) number[i] = nextint();


    for ( int i = 1 ; i < n ; i++ ){
        int now = number[i], j = i-1;
        while ( j >= 0 and number[j] > now ){
            number[j + 1] = number[j];
            --j;
        }
        number[j+1] = now;

        if ( i == 1 or i == 2 ) {
            for ( int k = 0 ; k <= i ; k++ ){
                cout << ( k == 0 ? "" : " " ) << number[k];
            }ENDL;
        }
    }9
    
    for ( int i = 0 ; i < n ; i++ ){
        cout << ( i == 0 ? "" : " " ) <<  number[i];
    }ENDL;
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