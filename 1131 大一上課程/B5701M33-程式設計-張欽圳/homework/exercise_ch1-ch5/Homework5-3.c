#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define Y "Yes\n"
#define N "No\n"
#define ll long long
#define MAX 1005
 
//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0 =  /0
//                    ___/`---__
//                  . \|     |// .
//                 / \|||  :  |||// 
//                / _||||| -:- |||||- 
//               |   | \ -  /// |   |
//               | |  '--/'  |_/ |
//                .-_  -  ___/-. /
//             ___. .  /--.-- `. .___
//          ."" <  `.___<|>_/___. > "
//         | | :  `- .;`_ /`;.`/ - ` : | |
//          `_.    __/__ _/   .-` /  /
//     =====`-.____`.___ ____/___.-`___.-=====
//                       `=---=
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//           佛祖保佑         一定Accepted
//
//
//

int nextint() {
    int x = 0, c = getchar(), neg = 0;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = 1, c = getchar();
    while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

ll gura[MAX][2] = { 0 };
ll reflect[MAX] = { 0 }; //映射表

int find( int n, int lucky ){
    int pos = -1;
    for ( int i = 0 ; i < n ; i++ ){
        if ( reflect[i] == lucky ){
            pos = i;
            break;
        }
    }
    return pos;
}

int solve(){
    ll n = 0, a = 0, b = 0;
    n = nextint();
    a = nextint(), b = nextint();
    int fptr = 0; // 維護頭指針
    for ( int i = 0 ; i < n ; i++ ){
        ll now = 0;
        now = nextint();
        int pos = 0;
        pos = find(n,now);
        if ( pos == -1 ){
            reflect[fptr] = now;
            gura[fptr][0] = now;
            ++gura[fptr][1];
            ++fptr;
        }else gura[pos][0] = now, ++gura[pos][1];
    }
    
    for ( int i = 0 ; i < n ; i++ ){
        for ( int j = 0 ; j < n-1-i ; j++ ){
            if ( gura[j][1] < gura[j+1][1] ){
                int value = 0, time = 0;
                value = gura[j][0], time = gura[j][1];
                gura[j][0] = gura[j+1][0], gura[j][1] = gura[j+1][1];
                gura[j+1][0] = value, gura[j+1][1] = time;
            }
        }
    }

    ll top = gura[0][1];

    int lim = 0;
    for ( int i = 0 ; i < n ; i++ ){
        if ( gura[i][1] == top ) ++lim;
        else break;
    };

    for ( int i = 0 ; i < lim ; i++ ){
        for ( int j = 0 ; j < lim-1-i ; j++ ){
            if ( gura[j][0] > gura[j+1][0] ){
                int value = 0, time = 0;
                value = gura[j][0], time = gura[j][1];
                gura[j][0] = gura[j+1][0], gura[j][1] = gura[j+1][1];
                gura[j+1][0] = value, gura[j+1][1] = time;
            }
        }
    }

    for ( int i = 0 ; i < lim ; i++ ) printf("%i appears %i times\n", gura[i][0], gura[i][1]);
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}