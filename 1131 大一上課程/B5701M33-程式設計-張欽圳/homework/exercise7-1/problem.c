#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>
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

int n = 0;
char binary[105];
char stringX[40], stringY[40], stringZ[40];

void gura( int n1, int n2 ){

    int ptr = 0;
    for ( int i = 0 ; i < n1 ; i++ ){
        stringX[ptr] = binary[i];
        ptr++;
    }stringX[ptr] = '\0',ptr = 0;

    for ( int i = n1 ; i < n1+n2 ; i++ ){
        stringY[ptr] = binary[i];
        ptr++;
    }stringY[ptr] = '\0',ptr = 0;

    for ( int i = n1+n2 ; i < n ; i++ ){
        stringZ[ptr] = binary[i];
        ptr++;
    }stringZ[ptr] = '\0',ptr = 0;
}

int solve(){
    n = nextint();
    for ( int i = 0 ; i < n ; i++ ) scanf("%c",&binary[i]);

    unsigned int x = 0, y = 0, z = 0;
    bool flag = 0;

    for ( int n1 = 0 ; n1 < n ; n1++ ){
        for ( int n2 = 0 ; n1+n2 < n ; n2++ ){
            int n3 = n-n1-n2;
            gura(n1, n2);

            x = strtoul(stringX,NULL,2);
            y = strtoul(stringY,NULL,2);
            z = strtoul(stringZ,NULL,2);

            if ( x > y && x + y == z ){
                flag = 1;
                break;
            }
        }
        if ( flag == 1 ) break;
    }
    printf("%u,%u,%u\n",x,y,z);

    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}