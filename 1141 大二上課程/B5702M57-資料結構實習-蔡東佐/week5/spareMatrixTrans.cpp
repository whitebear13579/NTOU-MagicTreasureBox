#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define int long long
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
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

inline signed solve(){
    int r = 0, c = 0, n = 0;
    r = nextint(), c = nextint(), n = nextint();
    spm matrix(n+1), matrixTrans;
    matrix[0] = { r, c, n };
    for ( int i = 1 ; i <= n ; i++ ){
        int nr = 0, nc = 0, val = 0;
        nr = nextint(), nc = nextint(), val = nextint();
        matrix[i] = {nr, nc, val};
    }

    matrixTrans = spmft(matrix, c, n);

    for ( auto i : matrixTrans ){
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