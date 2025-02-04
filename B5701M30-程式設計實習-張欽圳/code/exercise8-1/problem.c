#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define Y "Yes\n"
#define N "No\n"
 
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
    while('0' <= c && c <= '9') x = (x<<1)+(x<<3)+(c&15), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

int solve(){
    // Caution! 1 Based.
    int n = 0, p = 0;
    n = nextint();
    p = nextint();
    bool weeks[3655] = { 0 };
    for ( int i = 0 ; i < p ; i ++){
        int repeat = 0;
        repeat = nextint();
        for ( int days = repeat ; days <= n ; days = days + repeat ){
            if (days%7 == 6 || days%7 == 0)continue;
            weeks[days] = 1; // marks true.
        }
    }

    int lose = 0;
    for ( int i = 1 ; i <= n ; i++ ){
        if ( weeks[i] ) lose++;
    }
    printf("%i\n",lose);
    return 0;
}

int main(){
    int t = 0;
    t = nextint();
    while(t--)
        solve();
    return 0;
}