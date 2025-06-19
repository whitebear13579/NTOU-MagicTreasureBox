#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define Y "Yes\n"
#define N "No\n"
#define int long long int   
 
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

// important! 最高進位為36進位，最低是2進位

int WTF = 1;


signed solve( int t ){
    int cost[40] = { 0 };
    
    for ( int i = 0 ; i < 36 ; i++ ) cost[i] = nextint();
    int tc = 0;
    tc = nextint();
    printf("Case %lld:\n", WTF);
    WTF++;
    while ( tc-- ){
        int summary[40] = { 0 };
        int now = 0;
        int min = 0;
        now = nextint();
        for ( int i = 2 ; i <= 36 ; i++ ){
            int expressed = 0;
            int owo = 0;
            owo = now;
            while ( owo > 0 ){
                expressed = owo%i;
                summary[i] += cost[expressed];
                owo/=i; 
            }
            if ( min == 0 || summary[i] < min ) min = summary[i];
        }
        printf("Cheapest base(s) for number %lld:", now);
        for ( int i = 2 ; i <= 36 ; i++ ){
            if ( min == summary[i] ){
                printf(" %lld",i);
            }
        }
        if ( (t != 0) || ( t == 0 && tc != 0 ) )printf("\n");
    }
    if ( t > 0 ) printf("\n");
    return 0;
}

signed main(){
    int t = 0;
    t = nextint();
    while(t--){
        solve(t);
    }
    return 0;
}