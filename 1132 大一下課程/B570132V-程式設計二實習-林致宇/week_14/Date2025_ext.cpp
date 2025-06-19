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
        int magic = 2515;
        bool flag = 0;
        vi big{1, 3, 5, 7, 8, 10, 12};
        vi ref{0,31,28,31,30,31,30,31,31,30,31,30,31};
        vector<string> monthRef{"\0","January","February","March","April","May","June","July","August","September","October","November","December"};
        vector<string> weekRef{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","\0"};
        void illegal(){ m = -1, d = -1, isIllegal = 1; }
    public:
        bool isIllegal = 0;
        Date2025(){ m = 1, d = 1; }
        Date2025( int mm, int dd ){ // 假設不合法，則 m = -1, d = -1
            vi::iterator it = find(big.begin(), big.end(),mm);

            if ( mm < 1 or mm > 12 or dd < 1 ) illegal();
            else{
                if ( it != big.end() and dd >= 1 and dd <= 31 ) init;
                else if ( mm == 2 and dd <= 28 ) init;
                else if ( mm != 2 and dd <= 30 ) init;
                else illegal();
            }
        }
        
        void plusday(int days){
            while(days--){
                d++;
                if ( d > ref[m] ) m++, d = 1;
            }
        }

        inline int calcuteD(){
            int D = 0;
            for ( int i = 0 ; i < m ; i++ ) D += ref[i];
            D += d;
            return D;
        }

        void print(){
            if ( m == -1 and d == -1 and !flag ){
                cout << "a date not in 2025";
                flag = 1;
            }
            else{
                int D = 0;
                D = calcuteD();
                if ( flag == 0 ){
                    cout << weekRef[(magic+D)%7] << ", " << monthRef[m] << " " << d << ", " << "2025 is ";
                    flag = 1;
                }else cout << weekRef[(magic+D)%7] << ", " << monthRef[m] << " " << d << ", " << "2025.";
            }
        }
};
 
int main(int argc, char * argv[]) {
    int n;
    cin >> n;
    while(n--){
        Date2025 date;
        int m = 0, dm = 0, da = 0;
        cin >> m >> dm >> da;

        int checkD = 0;
        vi ref{0,31,28,31,30,31,30,31,31,30,31,30,31};
        for ( int i = 0 ; i < m ; i++ ) checkD += ref[i];
        checkD += dm;

        // 這裡只判斷da合不合法，剩下交給constructor判斷
        if ( checkD + da > 365  or da < 0){
            date = Date2025(-1,-1);
            date.print();
        }else{
            date = Date2025(m,dm);
            if (date.isIllegal){
                date = Date2025(-1,-1);
                date.print();
            }else{
                cout << da << " days after ";
                date.print();
                date.plusday(da);
                date.print();
                cout << "\n";
            }
        }   
    }
    return 0;
}