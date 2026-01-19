#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

struct poly{
    double coef;
    int expo; 
};

void formatOutput( vector<poly> uwu, int item ){
    for ( int i = 0 ; i < item ; i++ ){
        if ( i != 0 and uwu[i].coef >= 0 ) cout << "+";
        cout << fixed << setprecision(2) << uwu[i].coef;
        if ( uwu[i].expo > 1 ) cout << "x^" << uwu[i].expo;
        else if ( uwu[i].expo == 1 ) cout << "x";
    }
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
        if ( i->second != 0 ){
            if ( i != ans.rbegin() and i->second >= 0 ) cout << "+";
            cout << i->second;
            if ( i->first > 1 )cout << fixed << setprecision(2)<< "x^" << i->first;
            else if ( i->first == 1 ) cout << "x";
        }
    }
    cout << "\n";
}

inline signed solve(){
    int p1item = 0, p2item = 0;
    cin >> p1item;
    vector<poly> p1(p1item);
    for ( int i = 0 ; i < p1item ; i++ ) cin >> p1[i].coef >> p1[i].expo;
    formatOutput(p1,p1item);

    cin >> p2item;
    vector<poly> p2(p2item);
    for ( int i = 0 ; i < p2item ; i++ ) cin >> p2[i].coef >> p2[i].expo;
    formatOutput(p2,p2item);

    polyMulti(p1, p1item, p2, p2item);
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