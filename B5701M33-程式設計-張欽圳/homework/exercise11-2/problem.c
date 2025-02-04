#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
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
    int path = 0;
    int ext = 0;
    char router[1000];
    char changeName[500];

    scanf("%s",router);
    scanf("%s",changeName);
    int routerlen = strlen(router);
    bool isRecord = 0;
    for ( int i = routerlen - 1 ; i >= 0 ; i-- ){
        if ( !isRecord && router[i] == '.' ) ext = i, isRecord = 1;
        if ( router[i] == '\\' || router[i] == ':' ){
            path = i;
            break;
        }
    }
    if ( path ) for ( int i = 0 ; i <= path ; i++ ) printf("%c",router[i]);
    printf("%s",changeName);
    if ( ext ) for ( int i = ext ; i < routerlen ; i++ ) printf("%c",router[i]);
    printf("\n");
    return 0;
}

int main(){
    int t = 0;
    scanf("%i",&t);
    while(t--)
        solve();
    return 0;
}