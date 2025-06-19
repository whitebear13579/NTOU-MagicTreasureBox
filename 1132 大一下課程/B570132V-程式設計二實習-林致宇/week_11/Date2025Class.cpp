#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

class Date2025{
    private: // 假設該日期對於 2025 年不合法，則 m = -1, d = -1
        int m; // 月  (1–January, 2-February, …)
        int d; //日
        vi big{1, 3, 5, 7, 8, 10, 12};
        vi ref{0,31,28,31,30,31,30,31,31,30,31,30,31};
        vector<string> monthRef{"\0","January","February","March","April","May","June","July","August","September","October","November","December"};
        vector<string> weekRef{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","\0"};
        void illegal(){ m = -1, d = -1; }
    public:
        Date2025(){ m = 1, d = 1; }
        Date2025( int mm , int dd ){ // 假設不合法，則 m = -1, d = -1
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
        }
        void print(){
            int D = 0;
            for ( int i = 0 ; i < m ; i++ ) D += ref[i];
            D += d;
            if ( m == -1 and d == -1 ) cout << "a date not in 2025\n";
            else cout << weekRef[(2515+D)%7] << ", " << monthRef[m] << " " << d << ", " << "2025";
        }
};
 
int main(int argc, char * argv[]) {
    int n;
    cin >> n;
    for (int i = 0; i < n; i++) {
        int month, day;
        cin >> month;
        cin >> day;
        Date2025 date = Date2025(month, day);
        date.print();
        cout << endl;
    }
}