#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define Y "Yes\n"
#define N "No\n"
#define fer for ( int i = 0 ; i < n ; i++ )
 
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

const int GURA = 105;
const int used = -9999;
int a[GURA] = { 0 }, b[GURA] = { 0 };
int answer[GURA] = { 0 };
int n = 0;

int finder( int target ){
    int idx = 0;
    fer{
        if ( a[i] == target ){
            idx = i;
            a[i] = used;
            break;
        }
    }
    return idx;
}

int solve(){
    int input = 0;
    char ame = '\0';
    while( scanf("%i%c",&input,&ame) ){
        a[n] = input;
        n++;
        if ( ame == '\n' ) break;
    }
    fer scanf("%i%c",&b[i],&ame);
    fer answer[i] = finder(b[i]);
    fer printf( ( i == n-1 ? "%i\n" : "%i," ),answer[i] );
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}