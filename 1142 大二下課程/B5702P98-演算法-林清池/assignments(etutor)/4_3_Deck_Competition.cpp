// Category: Binary Search

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

vi check(200005,0);
int nowcut = 0;

bool limit (int tar, int k, vi &card, int n ){
    if ( tar == 0 ) return 1;
    int cut = 0, now = 0;
    ++nowcut;
    vi check(n+1,0);
    for ( auto i : card ){
        if ( i < tar ){
            if ( check[i] != nowcut ){
                check[i] = nowcut;
                ++now;
                if ( now == tar ){
                    ++cut;
                    ++nowcut;
                    now = 0;
                }
            }
        }
    }

    if ( cut >= k ) return 1;
    else return 0;
}

inline signed solve(){
    int n = 0, k = 0;
    n = nextint(), k = nextint();
    vi card(n,0);
    for ( int i = 0 ; i < n ; i++ ) card[i] = nextint();
    int l = 0, r = n, ans = 0;
    while ( l <= r ){
        int mid = (l+r)>>1;
        if ( limit( mid, k, card, n ) ){
            ans = mid;
            l = mid + 1;
        }else {
            r = mid - 1;
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