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

class Date{
    private:
        vi ref{0,31,28,31,30,31,30,31,31,30,31,30,31};
        vi big{1, 3, 5, 7, 8, 10, 12};
        void illegal(){ m = -1, d = -1; }
        void init(){
            C = 0;
            for ( int i = 0 ; i < m ; i++ ) C += ref[i];
            C += d;
        }
    protected:
        int y;
        int m;
        int d;
        int C;
        vector<string> monthRef{"\0","January","February","March","April","May","June","July","August","September","October","November","December"};
        vector<string> weekRef{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","\0"};
    public:
        // 當呼叫子物件時，會先呼叫父物件的 constructor!
        Date( int mm, int dd ){
            vi::iterator it = find(big.begin(), big.end(),mm);
            if ( it != big.end() ){ // in big
                if ( dd >= 1 and dd <= 31 ){
                    m = mm, d = dd;
                }else illegal();
            }else if ( mm >= 1 and mm <= 12){ // 確保月份合法
                if ( mm == 2 ){
                    if ( dd >= 1 and dd <= 28 ){
                        m = mm, d = dd;
                    }else illegal();
                }else{
                    if ( dd >= 1 and dd <= 30 ){
                        m = mm, d = dd;
                    }else illegal();
                }
            }else illegal();
            init();
        }
};

class Date2025 : public Date{
    private:
        int magic = 2515;
    public:
        Date2025( int mm, int dd ) : Date( mm, dd ){ y = 2025; }
        void output(){
            if ( m == -1 and d == -1 ) cout << "a date not in 2025\n";
            else cout << weekRef[(magic+C)%7] << ", " << monthRef[m] << " " << d << ", " << "2025\n";
        }
};

class Date2026 : public Date{
    private:
        int magic = 2516;
    public:
        Date2026( int mm, int dd ) : Date( mm, dd ){ y = 2026; }
        void output(){
            if ( m == -1 and d == -1 ) cout << "a date not in 2026\n";
            else cout << weekRef[(magic+C)%7] << ", " << monthRef[m] << " " << d << ", " << "2026\n";
        }
        
};

inline signed solve(){
    int m = 0, d = 0, y = 0;
    m = nextint(), d = nextint(), y = nextint();
    if ( y == 2025 ){
        Date2025 uwu(m,d);
        uwu.output();
    }else if ( y == 2026 ){
        Date2026 uwu(m,d);
        uwu.output();
    }
    return 0;
}

signed main(){
    whitebear;
    int t = 0;
    t = nextint();
    while(t--)
        solve();
    return 0;
}