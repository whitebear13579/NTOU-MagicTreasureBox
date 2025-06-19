#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;
#define fer for ( int i = 0 ; i < n ; i++ )
 
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

vi a,b;
map<int,vi> idxTable;
vii answer;
int n = 0;


void createTable( int n ){
    fer idxTable[a[i]].push_back(i);
}

void permutation ( int n, vi current, vector<bool> isUsed, int pos ){
    if ( pos == b.size() ){
        answer.push_back(current);
        return;
    }

    for ( auto idx : idxTable.at(b[pos]) ){
        if ( !isUsed[idx] ){
            current[pos] = idx;
            isUsed[idx] = 1;
            permutation(n, current, isUsed, pos+1);
            isUsed[idx] = 0;
        }
    }
}

inline signed solve(){
    int input = 0;
    char ame = '\0';
    while( scanf("%i%c",&input,&ame) ){
        a.push_back(input);
        n++;
        if ( ame == '\n' ) break;
    }
    fer{
        scanf("%i%c",&input,&ame);
        b.push_back(input);
    }
    createTable(n);
    vi current(n,0);
    vector<bool> isUsed(n,false);
    permutation( n, current, isUsed, 0);
    for ( auto i : answer ){
        for ( int j = 0 ; j < n ; j++ ){
            cout << i[j] << ( j == n-1 ? "\n" : "," );
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