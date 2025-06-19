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

const int MAX = 1005;

int nextint() {
    int x = 0, c = getchar(), neg = 0;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = 1, c = getchar();
    while('0' <= c && c <= '9') x = (x<<1)+(x<<3)+(c&15), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

int solve(){
    int n = 0;
    n = nextint();
    int gura[MAX] = { 0 };
    int tp = 0;
    for ( int i = 0 ; i < n ; i++ ){
        tp = nextint();
        gura[i] = tp;
    }

    int swp = 0;
    for ( int i = 0 ; i < n-1 ; i++ ){
        for ( int j = 0 ; j < n-1-i ; j++ ){
            if ( gura[j] > gura[j+1] ){
                int tmp = 0;
                tmp = gura[j+1];
                gura[j+1] = gura[j];
                gura[j] = tmp;
                swp++;
            }
        }
    }
    printf("%i\n",swp);
    for ( int i = 0 ; i < n ; i++ ){
        printf("%i\n",gura[i]);
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