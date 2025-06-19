#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define init m = mm, d = dd
using namespace std;

class Date2025{
    private: // 假設該日期對於 2025 年不合法，則 m = -1, d = -1
        int m; // 月  (1–January, 2-February, …)
        int d; //日
        vi big{1, 3, 5, 7, 8, 10, 12};
        void illegal(){ m = -1, d = -1; }
    public:
        Date2025(){ m = 1, d = 1; }
        Date2025( int mm, int dd ){
            vi::iterator it = find(big.begin(), big.end(),mm);

            if ( mm < 1 or mm > 12 or dd < 1 ) illegal();
            else{
                if ( it != big.end() and dd >= 1 and dd <= 31 ) init;
                else if ( mm == 2 and dd <= 28 ) init;
                else if ( mm != 2 and dd <= 30 ) init;
                else illegal();
            }
        }
        bool operator<(const Date2025 &b) const{
            if ( m != b.m ){
                return m < b.m;
            }else return d < b.d;
        }

        bool operator>(const Date2025 &b) const{
            if ( m != b.m ){
                return m > b.m;
            }else return d > b.d;
        }

        bool operator==(const Date2025 &b) const{
            return m == b.m and d == b.d;
        }
};
 
int main(int argc, char * argv[]) {
    int n;
    cin >> n;
    while(n--){
        int m1 = 0, m2 = 0, d1 = 0, d2 = 0;
        cin >> m1 >> d1 >> m2 >> d2;
        Date2025 a(m1,d1), b(m2,d2);
        if ( a > b ) cout << ">";
        else if ( a < b ) cout << "<";
        else if ( a == b ) cout << "=";

        cout << "\n";
    }
    return 0;
}