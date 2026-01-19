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

stack<int> miku;

inline pii getNow(){
    pii ouo;
    ouo.s = miku.top();
    miku.pop();
    ouo.f = miku.top();
    miku.pop();

    return ouo;
}


inline signed solve(){
    string uwu;
    getline(cin, uwu);
    //uwu = "4 5 * 2 +";
    bool ok = 1;

    for ( auto x : uwu ){

        if ( x == ' ' ) continue;

        if ( isdigit(x) ){
            miku.push(x-'0');
        }else{
            switch (x){
            case '+':
                if ( miku.size() >= 2 ){
                    auto [fir, sec] = getNow();
                    miku.push(fir+sec);
                }else ok = 0;
                break;
            case '-':
                if ( miku.size() >= 2 ){
                    auto [fir, sec] = getNow();
                    miku.push(fir-sec);
                }else ok = 0;
                break;
            case '*':
                if ( miku.size() >= 2 ){
                    auto [fir, sec] = getNow();
                    miku.push(fir*sec);
                }else ok = 0;
                break;
            case '/':
                if ( miku.size() >= 2 ){
                    auto [fir, sec] = getNow();
                    if ( sec == 0 ){
                        ok  = 0;
                        break;
                    }else miku.push(fir/sec);
                }else ok = 0;
                break;
            case '%':
                if ( miku.size() >= 2 ){
                    auto [fir, sec] = getNow();
                    if ( sec == 0 ){
                        ok  = 0;
                        break;
                    }else miku.push(fir%sec);
                }else ok = 0;
                break;
            
            default:
                break;
            }
        }
    }

    if ( miku.size() >= 2 ) ok = 0;
    cout << ( ok ? miku.top() : -1 ) << "\n";
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