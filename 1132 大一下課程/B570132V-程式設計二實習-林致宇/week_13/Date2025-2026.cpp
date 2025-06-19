#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define init m = mm, d = dd
using namespace std;

//usage cin >> n -> n = nextint()

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
        vi big{1, 3, 5, 7, 8, 10, 12};
        void illegal(){ m = -1, d = -1; }
    public:
        vector<string> monthRef{"\0","January","February","March","April","May","June","July","August","September","October","November","December"};
        vector<string> weekRef{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","\0"};
        vi ref{0,31,28,31,30,31,30,31,31,30,31,30,31};
        int C;
        int y;
        int m;
        int d;
        int magic;
        // 當呼叫子物件時，會先呼叫父物件的 constructor!
        Date( int mm, int dd ){
            vi::iterator it = find(big.begin(), big.end(),mm);

            if ( mm < 1 or mm > 12 or dd < 1 ) illegal();
            else{
                if ( it != big.end() and dd >= 1 and dd <= 31 ) init;
                else if ( mm == 2 and dd <= 28 ) init;
                else if ( mm != 2 and dd <= 30 ) init;
                else illegal();
            }
        }
};

class Date2025 : public Date{
    public:
        Date2025( int mm, int dd ) : Date( mm, dd ){ y = 2025, magic = 2515; }
};

class Date2026 : public Date{
    public:
        Date2026( int mm, int dd ) : Date( mm, dd ){ y = 2026, magic = 2516; }
};


void calculateDays( Date *tp ){
    tp->C = 0;
    for ( int i = 0 ; i < tp->m ; i++ ) tp->C += tp->ref[i];
    tp->C += tp->d;
}

void printDate( Date *tp ){
    if ( tp->m == -1 and tp->d == -1 ) cout << "a date not in " << tp->y << "\n";
    else cout << tp->weekRef[(tp->magic+tp->C)%7] << ", " << tp->monthRef[tp->m] << " " << tp->d << ", " << tp->y << "\n";
}

inline signed solve(){
    int m = 0, d = 0, y = 0;
    m = nextint(), d = nextint(), y = nextint();
    if ( y == 2025 ){
        Date2025 uwu(m,d);
        calculateDays(&uwu);
        printDate(&uwu);
    }else{
        Date2026 uwu(m,d);
        calculateDays(&uwu);
        printDate(&uwu);
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