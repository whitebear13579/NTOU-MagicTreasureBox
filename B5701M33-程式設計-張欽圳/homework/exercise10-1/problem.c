#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
#define int long long int
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

bool submit = 1;
int record[25][25] = { 0 };

unsigned long long move_bottle_horse(int red_bottle,int green_bottle){
    unsigned long long ans = 0;
    int k,r,g;

    /* boundary conditions */
    if ( red_bottle < 0 || green_bottle < 0 ) return 0;
    if ( record[red_bottle][green_bottle] != 0 ) return record[red_bottle][green_bottle];
    if ( red_bottle == 0 && green_bottle == 0 ) return 1;
    /* boundary conditions */
    
    /* recursive formula*/
    for(k = 1; k <= 3; ++k) {
        for(r = 0; r<=k; r++) {
            g = k-r; 
            ans+=move_bottle_horse(red_bottle-r,green_bottle-g);
        }
    }

    record[red_bottle][green_bottle] = ans;
    return ans;
}
unsigned long long move_bottle_driver(int red_bottle,int green_bottle){
    return move_bottle_horse(red_bottle,green_bottle-1)+    
           move_bottle_horse(red_bottle,green_bottle-2)+
           move_bottle_horse(red_bottle,green_bottle-3);
}

int solve( int nr , int ng ){
    printf("%llu\n",move_bottle_driver(nr,ng));
    return 0;
}

signed main(){
    int nr = 0, ng = 0;
    if ( submit ){
        while ( scanf("%lld %lld",&nr,&ng) != EOF ){
            solve(nr,ng);
        }
    }else nr = nextint(), ng = nextint(), solve(nr,ng);
    return 0;
}