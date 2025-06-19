#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#define insert gura[x][y] = element, ++element
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

const int MAX = 100005;
int gura[MAX][MAX];
int up[2] = { -1, 1 };
int down[2] = { 1, -1 };
int edgeD[2] = { 1, 0 };
int edgeR[2] = { 0, 1 };
int findX = 0, findY = 0;

int neiborFind[4][2] = {{1,0}, {-1,0}, {0,1}, {0,-1}};

void findElement( int n ){
    bool flag = 0;
    for ( int i = 0 ; i < MAX ; i++ ){
        for ( int j = 0 ; j < MAX ; j++ ){
            if ( gura[i][j] == n ){
                findX = i, findY = j;
                flag = 1;
                break;
            }
        }
        if ( flag )break;
    }
}

void createMap(){
    int x = 0, y = 0;
    int element = 1;
    gura[x][y] = element, ++element;
    while ( element <= 1e8 ){
        x += edgeR[0], y += edgeR[1];
        insert;
        while( y != 0 ){
            x += down[0], y += down[1];
            insert;
        }
        x += edgeD[0], y += edgeD[1];
        insert;
        while ( x != 0 ){
            x += up[0], y += up[1];
            insert;
        }
    }
}

int cmp ( const void *a , const void *b ){ 
	return *(int *)a > *(int *)b; 
}

int solve(){
    createMap();
    int n = 0;
    while ( n = nextint(), n != 0 ){
        findElement(n);
        int neighborhod[4] = { 0 };
        for ( int i = 0 ; i < 4 ; i++ ){
            int x = 0, y = 0;
            x = findX, y = findY;
            if ( x + neiborFind[i][0] >= 0 && x + neiborFind[i][0] < MAX && y + neiborFind[i][1] >= 0 && y + neiborFind[i][1] < MAX ){
                x += neiborFind[i][0];
                y += neiborFind[i][1];
                neighborhod[i] = gura[x][y];
            }
        }
        qsort(neighborhod, 4, sizeof(neighborhod[0]), cmp);
        for ( int i = 0 ; i < 4 ; i++ ){
            if ( neighborhod[i] == 0 ) continue;
            printf( ( i == 3 ? "%i\n" : "%i," ), neighborhod[i] );
        }
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