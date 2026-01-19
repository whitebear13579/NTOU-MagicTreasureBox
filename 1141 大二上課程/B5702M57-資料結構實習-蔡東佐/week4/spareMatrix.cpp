#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define tiii tuple<int,int,int>
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

bool compare( tiii a, tiii b ){
    if( get<1>(a) == get<1>(b) ) return get<0>(a) < get<0>(b);
    else return get<1>(a) < get<1>(b);
}


inline signed solve(){
    //稀疏矩陣轉置
    int r = 0, c = 0, n = 0;
    r = nextint(), c = nextint(), n = nextint();
    vector<tiii> db(n+1);
    db[0] = {r, c, n};
    for ( int i = 1 ; i <= n ; i++ ){
        int nr = 0, nc = 0, val = 0;
        nr = nextint(), nc = nextint(), val = nextint();
        db[i] = {nr, nc, val};
    }
    sort(db.begin()+1, db.end(), compare);

    for ( int i = 1 ; i <= n ; i++ ){
        swap(get<0>(db[i]), get<1>(db[i]));
    }
    
    for ( auto i : db ){
        cout << get<0> (i) << " " << get<1> (i) << " " << get<2> (i) << "\n";
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