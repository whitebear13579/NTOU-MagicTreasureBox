#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define Y "YES"
#define N "NO"
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
    int n = 0, v = 0;
    n = nextint();
    while ( v = nextint() ){
        bool isPossible = 1;
        vi train;
        stack<int> st;
        if ( v == -1 ) return 0;
        else{
            train.push_back(v);
            for ( int i = 0 ; i < n-1 ; i++ ){
                int tp = nextint();
                train.push_back(tp);
            }
            for ( int i = 0, j = 1; i < n ; i++ ){
                while ( j <= train[i] ){
                    st.push(j);
                    ++j;
                }
                if ( !st.empty() ){
                    if ( st.top() == train[i] ) st.pop();
                    else{
                        isPossible = 0;
                        break;
                    }
                }
                
                if ( j > n && st.empty() ) break;
            }

        }
        cout << (isPossible ? Y : N) << "\n";
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