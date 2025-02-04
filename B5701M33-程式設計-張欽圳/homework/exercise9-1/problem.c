#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
#define Y "Yes\n"
#define N "No\n"
#define fir for ( int i = 5 ; i < m+5 ; i++ )
#define fjr for( int j = 5 ; j < m+5 ; j ++ )
#define int long long
 
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

const int M = 120;

int dx[8] = { -1, -1, -1, 0, 1, 1,  1,  0 };
int dy[8] = { -1,  0,  1, 1, 1, 0, -1, -1 };

bool isInLand ( int before[][M] ,int i , int j ){
    for ( int k = 0 ; k < 8 ; k++ ){
        if ( before[i+dx[k]][j+dy[k]] == 0 ) return 0;
    }
    return 1;
}

int solve(){
    int before[M][M] = { 0 };
    int after[M][M] = { 0 };
    int m = 0;
    int ans = 0;
    scanf("%i",&m);

    fir fjr scanf("%1i",&before[i][j]);

    fir{
        fjr{
            if ( isInLand(before,i,j) && before[i][j] != 0 ) after[i][j] = 1;
            else after[i][j] = 0;
        }
    }

    for ( int i = 0 ; i < M ; i++ ){
        for ( int j = 0 ; j < M ; j++ ){
            if ( after[i][j] == 1 ) ans++;
        }
    }
    printf("%i\n",ans);
    /*
    fir{
        fjr{
            printf("%i ",before[i][j]);
        }
        printf("%i\n");
    }
    printf("---------------------------------------------------\n");
    fir{
        fjr{
            printf("%i ",after[i][j]);
        }
        printf("%i\n");
    }*/
    return 0;
}

signed main(){
    int t = 0;
    scanf("%i",&t);
    while(t--)
        solve();
    return 0;
}