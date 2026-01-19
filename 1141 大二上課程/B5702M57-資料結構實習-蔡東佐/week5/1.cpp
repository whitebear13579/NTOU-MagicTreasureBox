#include <iostream>
#include <vector>
#include <algorithm>
#include <set>
#include <unordered_map>
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

set<tiii> operator* (const spm a , const spm b){
    // transposed multi!
    set<tiii> ans;
    
    //ans.insert({ get<0>(a[0]), get<0>(b[0]), 0 });

    auto mhash = [](const pii &p){
        return hash<int>()(p.f) ^ (hash<int>()(p.s) << 1);
    };

    unordered_map<pii, int, decltype(mhash)> rsm(0,mhash);

    for ( int i = 1 ; i <= get<2>(a[0]) ; ++i  ){
        for ( int j = 1 ; j <= get<2>(b[0]) ; ++j ){
            if ( get<1>(a[i]) == get<1>(b[j]) ){
                rsm[{get<0>(a[i]), get<0>(b[j])}] += get<2>(a[i]) * get<2>(b[j]);
            }
        }
    }

    for ( auto i : rsm ){
        if ( i.s != 0 ){
            ans.insert({i.f.f, i.f.s, i.s});
        }
    }
    return ans;
}

inline signed solve(){
    int n = 0;
    cin >> n;
    spm a(n+1);
    int maxR = -1, maxC = -1;
    for ( int i = 1 ; i <= n ; i++ ){
        int tr = 0, tc = 0, val = 0;
        char ctp;
        cin >> ctp >> tr >> ctp >> tc >> ctp >> ctp >> val;
        a[i] = { tr, tc, val };
        maxR = max(tr, maxR);
        maxC = max(tc, maxC);
    }
    a[0] = {maxR, maxC, n};

    auto ans = a*a;
    
    for ( auto it : ans ){
        cout << get<0>(it) << " " << get<1>(it) <<" "<< get<2>(it) << "\n";
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