#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
#define Y "Yes\n"
 
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

int gura[15] = { 0 };
int n = 0, m = 0;

bool checker ( int ans[] ){
    int needeven = 0;
    needeven = ( n%2 == 0 ? n/2 : (n/2)+1 );
    for ( int i = 0 ; i < needeven ; i++ ){
        // 達到需要的偶數長度前出現奇數，回傳 false.
        if ( ans[i]%2 == 0 ) return 0;
    }
    return 1;
}

void combine ( int now[], int start, int end, int idx ){
    if ( idx == n && checker(now) ){
        for ( int i = 0 ; i < n ; i++ ) printf( ( i == n-1 ? "%i\n" : "%i," ),now[i] );
        //memset(now,0,15);
        return;
    }

    for ( int i = start ; i <= end && end - i + 1 >= n - idx ; i++ ){
        now[i] = gura[i];
        combine( now, i+1, end, idx + 1 );
    }
}

int solve(){
    m = nextint() , n = nextint();
    for ( int i = 0 ; i < m ; i++ ) gura[i] = nextint();
    int data[15] = { 0 };
    combine( data, 0, m-1, 0 );
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}