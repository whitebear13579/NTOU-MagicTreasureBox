// Category: Greedy

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

struct uwu{
    int id;
    int days;
    int fine;
};

bool cmp( uwu a, uwu b ){
    if ( a.fine * b.days == b.fine * a.days ) return a.id < b.id;
    return a.fine * b.days > b.fine * a.days;
}

inline signed solve(){
    int n = 0, fine = 0, d = 0;
    n = nextint();
    vector<uwu> order;
    for ( int i = 0 ; i < n ; i++ ){
        d = nextint(), fine = nextint();
        order.push_back({ (i+1), d, fine });
    }
    sort(order.begin(), order.end(), cmp);
    for ( int i = 0 ; i < n ; i++ ) cout << order[i].id << " ";
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