#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
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

signed cmp ( const void* a, const void* b ){
    return *(int *)a > *(int *)b;
}

const int MAX = 2e10;

int solve(){
    int n = 0;
    while ( n = nextint(), n != 0 ){
        int before = 0, after = 0;
        int line = 1, sum = 0;
        if ( n <= 2){ // special case
            printf(( n == 1 ? "2,3\n" : "1,5,6\n"));
        }else{
            while ( 1 ){ // get before and after.
                sum += line;
                before = sum;
                line++;
                if ( sum + line >= n ){
                    after = sum + line;
                    break;
                }
            }
            int neighbor[4] = { 0 };
            int ptr = 0;
            bool edge = 0;
            if ( n == after || n-1 == before ) edge = 1;

            // Next Line ( down, right)
            neighbor[ptr] = after+1+abs(after - n);
            ptr++;
            neighbor[ptr] = neighbor[ptr-1]+1;
            ptr++;

            // before line (up, left)
            // edge case doesn't existed N+1
            if ( edge ){
                int tp = abs(before+1 - n);
                if ( !tp ) neighbor[ptr] = before;
                else neighbor[ptr] = before+1 - tp;
            }else{
                neighbor[ptr] = before - abs(before+1 - n);
                ptr++;
                neighbor[ptr] = neighbor[ptr-1]+1;
            }

            for ( int i = 0 ; i < 4 ; i++ ){
                if ( neighbor[i] == 0 ) neighbor[i] = MAX;
            }
            qsort(neighbor, 4, sizeof(neighbor[0]), cmp);
            for ( int i = 0 ; i <= ptr ; i++ ){
                printf( ( i == ptr ? "%lld\n" : "%lld," ),neighbor[i]);
            }
            
        }
    }
    return 0;
}

signed main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}