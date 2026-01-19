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
inline int nextint() {
    int x = 0, c = getchar(), neg = false;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = true, c = getchar();
    while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

struct poly{
    double coef;
    int expo; 
};

int compareExpo ( poly a, poly b ){
    if ( a.expo > b.expo ) return -1;
    else if ( a.expo == b.expo ) return 0;
    else if ( a.expo < b.expo ) return 1;
}

void polyAdd( vector<poly> a, int aItem, vector<poly> b, int bItem ) {
    vector<poly> ans;
    int i = 0, j = 0;
    while ( i < aItem and j < bItem ) {
        int rsCE = compareExpo(a[i], b[i]);
        if ( rsCE == 0 ){
            double newCoef = a[i].coef + b[i].coef;
            if ( newCoef != 0 ) ans.push_back({newCoef, a[i].expo});
            ++i, ++j;
        }else if ( rsCE == -1 ) {
            ans.push_back(a[i]);
            ++i;
        }else if ( rsCE == 1 ) {
            ans.push_back(b[i]);
            ++j;
        }
    }

    for ( ; i < aItem ; ++i ) ans.push_back(a[i]);
    for ( ; j < bItem ; ++j ) ans.push_back(b[j]);
    
    for ( auto i : ans ) cout << i.coef << " " << i.expo << " ";
    cout << "\n";
}

void polyMulti( vector<poly> a, int aItem, vector<poly> b, int bItem ) {
    map<int,double> ans;
    for ( int i = 0 ; i < aItem ; i++ ){
        for ( int j = 0 ; j < bItem ; j++ ){
            int nowExp = a[i].expo + b[j].expo;
            double nowCoef = a[i].coef * b[j].coef;
            if ( ans.find(nowExp) == ans.end() ) ans[nowExp] = nowCoef;
            else ans[nowExp] += nowCoef;
        }
    }
    
    for ( auto i = ans.rbegin() ; i != ans.rend() ; i++ ){
        if ( i->second != 0 ) cout << i->second << " " << i->first << " ";
    }
    cout << "\n";
}

inline signed solve(){
    int ops = 0;
    int p1item = 0, p2item = 0; // 兩poly項數
    ops = nextint(); // 1 加法 2 乘法
    p1item = nextint(), p2item = nextint();
    vector<poly> p1(p1item) , p2(p2item);
    for ( int i = 0 ; i < p1item ; i++ ) p1[i].coef = nextint(), p1[i].expo = nextint();
    for ( int i = 0 ; i < p2item ; i++ ) p2[i].coef = nextint(), p2[i].expo = nextint();

    if ( ops == 1 ) polyAdd(p1, p1item, p2, p2item);
    else if ( ops == 2 ) polyMulti(p1, p1item, p2, p2item);
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