#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
#define Y "correct\n"
#define N "erroneous\n"
 
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

//odd parity check

int solve(){
    unsigned char x;
    while( scanf("%u",&x) != EOF ){
        int total = 0;
        for ( int i = 0 ; i < 8 ; i++ ){
            if ( x & (1<<i) ) total++;
            /*
                檢查 unsigned char x 的第 i 為是否為 1，並跟 1 做 AND
                1 << i 產生只有第 i 位為 1 的 bit patterns, e.g. 00100000 , i == 5
                x 與 該 bit patterns AND 運算，就可得知結果（0或非零）
            */
        }
        if ( total%2 == 1 ) printf(Y);
        else printf(N);
    }
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}