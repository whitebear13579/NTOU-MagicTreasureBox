#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define pb push_back
#define clear fill(ans.begin(), ans.end(), 0), ptr = 0
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

vi even;
vi odd;

void output( vi ans ){
    for ( int i = 0 ; i < ans.size() ; i++ ){
        cout << ans[i] << ( i == ans.size()-1 ? "\n" : "," );
    }
}

inline signed solve(){
    int m = 0, n = 0;
    m = nextint(), n = nextint();
    for ( int i = 0 ; i < m ; i++ ){
        int tp = 0;
        tp = nextint();
        ( tp%2 == 0 ? even.pb(tp) : odd.pb(tp) );
    }

    for( int needeven = ( n%2 == 0 ? n/2 : (n/2)+1 ) ; needeven <= even.size() ; needeven++ ){
        vi ans(n,0);
        int ptr = 0;
        vector<bool> evenbin(even.size(),0);
        vector<bool> oddbin(odd.size(),0);
        for ( int i = 0 ; i < needeven ; i++ ) evenbin[i] = 1;
        int needodd = n - needeven;
        for ( int i = 0 ; i < needodd ; i++ ) oddbin[i] = 1;
        
        for ( int i = 0 ; i < even.size() ; i++ ){
            if ( evenbin[i] == 1 ) ans[ptr] = even[i], ptr++;
        }
        for ( int i = 0 ; i < odd.size() ; i++ ){
            if ( oddbin[i] == 1 ) ans[ptr] = odd[i], ptr++;
        }
        output(ans);
        clear;
        while ( prev_permutation( oddbin.begin(), oddbin.end() ) ){
            for ( int i = 0 ; i < even.size() ; i++ ){
                if ( evenbin[i] == 1 ) ans[ptr] = even[i], ptr++;
            }
            for ( int i = 0 ; i < odd.size() ; i++ ){
                if ( oddbin[i] == 1 ) ans[ptr] = odd[i], ptr++;
            }
            output(ans);
            clear;
        }
        
        while ( prev_permutation( evenbin.begin(), evenbin.end() ) ){
            do{
                for ( int i = 0 ; i < even.size() ; i++ ){
                    if ( evenbin[i] == 1 ) ans[ptr] = even[i], ptr++;
                }
                for ( int i = 0 ; i < odd.size() ; i++ ){
                    if ( oddbin[i] == 1 ) ans[ptr] = odd[i], ptr++;
                }
                output(ans);
                clear;
            }while ( prev_permutation( oddbin.begin(), oddbin.end() ) );
        }
        

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