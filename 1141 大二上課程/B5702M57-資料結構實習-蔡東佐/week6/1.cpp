#include <iostream>
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

const int MIN = -99999999;

class cqueue{
    private:
        int size;
        int front = 0 , end = 0;
        int db[10000];
    public:
        void init( int v ){
            size = v;
        }

        bool isFull(){
            return ( end + 1 )%size == front;
        }

        bool isEmpty(){
            return front == end;
        }

        int push( int v ){
            if ( isFull() ){
                return -1;
            }else {
                db[end] = v;
                end = ( end + 1 )%size;
                return 1;
            }
        }

        int pop() {
            if ( isEmpty() ){
                return MIN;
            }else{
                int val = db[front];
                front = ( front + 1 ) % size;
                return val;
            }
        }
};

inline signed solve(){
    int s = 0;
    s = nextint();
    cqueue cycle;
    cycle.init(s);
    int ops = 0, val = 0;
    while( 1 ){
        ops = nextint();
        if ( ops == -1 ) break;
        else if ( ops == 0 ) {
            int cnd = cycle.pop();

            if ( cnd == MIN ){
                cout << "EMPTY\n";
            }else cout << cnd << "\n";
            
        }else if ( ops == 1 ){
            val = nextint();
            int cnd = cycle.push(val);

            if ( cnd == -1 ){
                cout << "FULL\n";
            }
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