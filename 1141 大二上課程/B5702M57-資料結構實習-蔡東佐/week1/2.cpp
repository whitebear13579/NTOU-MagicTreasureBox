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
    map<int,int> rec;
    int st = 2;
    if ( n <= 2 ){
        cout << n << "\n";
        return 0;
    }
    while ( st != n ){
        if ( n%st == 0 ){
            n = n / st;
            if (rec.find(st) == rec.end()){
                rec[st] = 1;
            }else rec[st]++;
        }else st++;
    }
    if (rec.find(n) == rec.end()){
        rec[n] = 1;
    }else rec[n]++;
    bool flag = 1;
    for ( auto i : rec ){
        if (flag) flag = 0;
        else cout << " * ";

        if ( i.s > 1 ){
            cout << i.f << "^" << i.s;
        }else cout << i.f;
    }
    cout << "\n";
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