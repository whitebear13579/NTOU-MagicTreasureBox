#include <iostream>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

class cStack{
    private:
        int top = -1;
        bool arr[51];
    public:
        bool empty(){
            if ( top == -1 ) return true;
            else return false;
        }

        void push(){
            if ( top < 50 ){
                ++top;
                arr[top] = 1;
            }else return;
        }

        void pop(){
            if ( !empty() ){
                arr[top] = 0;
                --top;
            }else return;
        }
};

inline int solve( string ouo ){
    cStack uwu;
    bool flag = true;
    for ( int i = 0 ; i < ouo.length() ; i++ ){
        if ( ouo[i] == '(' ) uwu.push();
        else if ( ouo[i] == ')' and !uwu.empty() ) uwu.pop();
        else if ( ouo[i] == ')' and uwu.empty() ){
            flag = false;
            break;
        }
    }
    if ( !uwu.empty() ) flag = false;

    if ( flag ) return 1;
    else return -1;
}

signed main(){
    whitebear;
    string ouo;
    while ( cin >> ouo ){
        if ( ouo == "end" ) break;
        cout << solve(ouo) << "\n";
    }
    return 0;
}