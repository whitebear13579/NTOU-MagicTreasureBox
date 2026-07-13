// Category: Dynamic Programming

#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

/*
    3 operations: 
    - delete: 消耗一個原始字，目標字不動 -> 從上方轉移過來
    - insertion: 原始字不動，產一個目標字 -> 從左邊轉移過來
    - replace: 消耗一個原始字，同時產一個目標字 -> 從斜方轉移過來

*/

inline signed solve(){
    string orig, targ;
    cin >> orig >> targ;
    vii dp( orig.length()+5, vi(targ.length()+5, 0) );
    for ( int i = 1 ; i <= orig.length() ; ++i ) dp[i][0] = i;
    for ( int i = 1 ; i <= targ.length() ; ++i ) dp[0][i] = i;
    for ( int i = 1 ; i <= orig.length() ; ++i ){
        for ( int j = 1 ; j <= targ.length() ; ++j ){
            if ( orig[i-1] == targ[j-1] ){
                // 目標與原始相等，不變
                dp[i][j] = dp[i-1][j-1];
            }else{
                // 不相等，三操作取min + 1
                dp[i][j] = min( min(dp[i-1][j], dp[i][j-1]), dp[i-1][j-1] ) + 1;
            }
        }
    }
    cout << dp[orig.length()][targ.length()] << "\n";
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