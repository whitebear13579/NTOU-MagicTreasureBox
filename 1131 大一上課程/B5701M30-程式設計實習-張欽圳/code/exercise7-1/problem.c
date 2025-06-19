#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define Y "Yes\n"
#define N "No\n"
#define gura for ( int i = 0 ; i < padding ; i++ ) origInput[ptr] = ORZ , ptr++

const int ORZ = -999999.0;

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

double origInput[100] = { 0 };
double outOK[100] = { 0 };
int outlen = 0;

int nextint() {
    int x = 0, c = getchar(), neg = 0;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = 1, c = getchar();
    while('0' <= c && c <= '9') x = (x<<1)+(x<<3)+(c&15), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

double findmax( int k, int strike, int pool_size ){
    int l = k*strike;
    int r = k*strike+(pool_size-1);
    double max = -999999;
    for ( int i = l ; i <= r ; i++ ){
        if ( origInput[i] > max ) max = origInput[i];
    }
    return max;
}

int solve(){
    int n = 0, padding = 0, pool_size = 0, strike = 0;
    n = nextint(), padding = nextint(), pool_size = nextint() , strike = nextint();
    int ptr = 0;
    outlen = (( n + 2 * padding - pool_size )/strike) + 1;

    // filling -wuxianda and save the input :D
    gura;
    for ( int i = 0 ; i < n ; i++ ) scanf("%lf",&origInput[ptr]), ptr++;
    gura;

    for ( int k = 0 ; k < outlen ; k++ ){
        outOK[k] = findmax(k, strike, pool_size);
    }

    for ( int i = 0 ; i < outlen ; i++ ){
        printf("%3d,%8f\n",i,outOK[i]);
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