// Category: Divide and Conquer

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

int inv_pair = 0;

void merge( vi &arr, int l, int m, int r ){
    int n1 = m - l + 1, n2 = r - m;
    vi LEFT(n1+1, 0), RIGHT(n2+1,0);
    for ( int i = 0 ; i < n1 ; i++ ){
        LEFT[i] = arr[l+i];
    }
    for ( int i = 0 ; i < n2 ; i++ ){
        RIGHT[i] = arr[m+i+1];
    }
    LEFT[n1] = INT_MAX, RIGHT[n2] = INT_MAX;
    int ptr1 = 0, ptr2 = 0;
    for ( int i = l ; i <= r ; i++  ){
        if ( LEFT[ptr1] <= RIGHT[ptr2] ){
            arr[i] = LEFT[ptr1];
            ++ptr1;
        }else { // l > r, inv pair
            arr[i] = RIGHT[ptr2];
            ++ptr2;
            inv_pair += n1-ptr1;
        }
    }
}

void mergesort( vi &arr, int l, int r ){
    if ( l < r ){
        int m = (l+r)>>1;
        //cout << "Now Round: " << l << " " << m << " " << r << "\n";
        mergesort( arr, l, m );
        mergesort( arr, m+1, r );
        merge( arr, l, m, r );
        /*
        cout << "Now sort: ";
        for ( auto it = arr.begin() ; it != arr.end() ; it++ ){
            cout << *it << " ";
        }cout << "\n-------------------------------------------\n";*/
    }
}

inline signed solve(){
    int n = 0;
    n = nextint();
    vi tomorin(n,0);
    for ( int i = 0 ; i < n ; i++ ) tomorin[i] = nextint();
    mergesort( tomorin, 0, n-1 );
    cout << inv_pair << "\n";
    /*for ( int i = 0 ; i < n ; i++ ){
        cout << tomorin[i] << " ";
    }cout << "\n";*/
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