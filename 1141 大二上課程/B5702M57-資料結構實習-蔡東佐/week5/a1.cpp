#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
#define int long long
using namespace std;

struct obj{
    char name;
    double val;
    double weight;
};

inline signed solve(){
    double backWei = 0, backVal = 0;
    stack<obj> backpack;

    for ( int t = 0 ; t < 5 ; t++ ){
        obj now;
        cin >> now.name >> now.val >> now.weight;
        if ( backWei + now.weight <= 20.0 ){
            backWei += now.weight;
            backVal += now.val;
            backpack.push(now);
        }else{
            stack<obj> tmp;
            bool swp = false;
            while ( !backpack.empty() ){
                auto nowTop = backpack.top();
                if ( nowTop.val < now.val and backWei - nowTop.weight + now.weight <= 20.0 ){
                    backWei -= nowTop.weight, backVal -= nowTop.val;
                    backpack.pop(); // 先丟
                    while ( !tmp.empty() ){ // 再來先把路邊東西倒回去
                        backpack.push(tmp.top());
                        tmp.pop();
                    } // 最後才拿進來 ㄜ盒
                    backWei += now.weight , backVal += now.val;
                    backpack.push(now);
                    swp = true;
                    break;
                }else{
                    tmp.push(backpack.top());
                    backpack.pop();
                }
            }

            if ( !swp ){
                while ( !tmp.empty() ){
                    backpack.push(tmp.top());
                    tmp.pop();
                }
            }
        }
    }

    while ( !backpack.empty() ){
        cout << fixed << setprecision(1) << backpack.top().name << " " << backpack.top().val << " " << backpack.top().weight << "\n";
        backpack.pop();
    }
    cout << "weight:" << backWei << "\n" << "value:" << backVal << "\n";
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