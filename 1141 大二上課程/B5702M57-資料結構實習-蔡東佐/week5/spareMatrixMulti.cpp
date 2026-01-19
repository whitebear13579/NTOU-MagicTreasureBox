#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define int long long
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define pb push_back
#define tiii tuple<int,int,int>
#define spm vector<tiii>
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

spm spmft ( spm a, int c, int n ){
    /* .f = colTerms, .s = startingPos */
    spm b(n+1);
    int r = get<0>(a[0]);
    b[0] = {c, r, n};
    
    vector<pii> pre(c+1);
    for ( int i = 0; i <= c; i++ ) pre[i].f = 0; // initialize to 0
    for ( int i = 1 ; i <= n ; i++ ) pre[get<1>(a[i])].f++;
    pre[0].s = 1;
    for ( int i = 1 ; i <= c ; i++ ) pre[i].s = pre[i-1].s + pre[i-1].f;

    for ( int i = 1 ; i <= n ; i++ ){
        int col = get<1>(a[i]), q = pre[col].s;
        get<0>(b[q]) = get<1>(a[i]);
        get<1>(b[q]) = get<0>(a[i]);
        get<2>(b[q]) = get<2>(a[i]);
        pre[col].s++;
    }
    return b;
}

spm operator* (const spm a , const spm b){
    spm ans;
    
    ans.pb({ get<0>(a[0]), get<0>(b[0]), 0 });
    map<pii, int> rsm;

    for ( int i = 1 ; i <= get<2>(a[0]) ; ++i  ){
        for ( int j = 1 ; j <= get<2>(b[0]) ; ++j ){
            if ( get<1>(a[i]) == get<1>(b[j]) ){
                rsm[{get<0>(a[i]), get<0>(b[j])}] += get<2>(a[i]) * get<2>(b[j]);
            }
        }
    }

    int cnt = 0;
    for ( auto i : rsm ){
        if ( i.s != 0 ){
            ans.pb({i.f.f, i.f.s, i.s});
            ++cnt;
        }
    }
    get<2>(ans[0]) = cnt;
    return ans;
}

inline signed solve(){
    int r1 = 0, c1 = 0, n1 = 0, r2 = 0, c2 = 0, n2 = 0;
    //matrix1 in
    r1 = nextint(), c1 = nextint(), n1 = nextint();
    spm a(n1+1);
    for ( int i = 1 ; i <= n1 ; i++  ){
        int tr = 0, tc = 0, tv = 0;
        tr = nextint(), tc = nextint(), tv = nextint();
        a[i] = {tr, tc, tv};
    }
    a[0] = {r1, c1, n1};

    //matrix2 in
    r2 = nextint(), c2 = nextint(), n2 = nextint();
    spm b(n2+1);
    b[0] = {r2, c2, n2};
    for ( int i = 1 ; i <= n2 ; i++ ){
        int tr = 0, tc = 0, tv = 0;
        tr = nextint(), tc = nextint(), tv = nextint();
        b[i] = {tr, tc, tv};
    }

    auto ans = a*spmft(b, c2, n2);
    
    for ( auto i : ans ) {
        cout << get<0>(i) << " " << get<1>(i) << " " << get<2>(i) << "\n";
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