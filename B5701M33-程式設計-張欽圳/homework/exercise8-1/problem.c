#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
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

// 黑色先下棋
// black 1 , white 2

bool haveWinner = 0;
int winner = 0;
int board[30][30] = { 0 };

void everyTimeOut( int y , int x ){
    if ( haveWinner == 0 ){
        int counter = 0;
        for ( int i = 0 ; i < 30 ; i++ ) if ( board[y][i] == 1 || board[y][i] == 2 ) counter++;
        printf("%i:%i ",y-5,counter);
        counter = 0;
        for ( int i = 0 ; i < 30 ; i++ ) if ( board[i][x] == 1 || board[i][x] == 2 ) counter++;
        printf("%i:%i\n",x-5,counter);
    }
}

void checker( int elements[] ){
    for ( int i = 0 ; i < 15 ; i++ ){
        if ( elements[i] == 1 && elements[i+1] == 1 && elements[i+2] == 1 && elements[i+3] == 1 && elements[i+4] == 1 ) haveWinner = 1, winner = 1;
        if ( elements[i] == 2 && elements[i+1] == 2 && elements[i+2] == 2 && elements[i+3] == 2 && elements[i+4] == 2 ) haveWinner = 1, winner = 2;
    }
}

void generater( int y, int x ){
    int ptr = 0;
    //上下 左右 兩斜

    //up and down
    int checkListY[20] = { 0 };
    for ( int i = y-6 ; i < y + 6 ; i++ ){
        checkListY[ptr] = board[i][x];
        ptr++;
    }
    checker(checkListY);
    if ( haveWinner ) return;

    //left and right
    int checkListX[20] = { 0 };
    ptr = 0;
    for ( int i = x-6 ; i < x+6 ; i++ ){
        checkListX[ptr] = board[y][i];
        ptr++;
    }
    checker(checkListX);
    if ( haveWinner ) return;
    
    //Slash (m = -1)
    int checkListSlash[20] = { 0 };
    ptr = 0;
    for ( int i = y-6, j = x-6 ; i < y+6 ; i++,j++ ){
        checkListSlash[ptr] = board[i][j];
        ptr++;
    }
    checker(checkListSlash);
    if ( haveWinner ) return;

    //Slash (m = 1)
    int checkListSlash2[20] = { 0 };
    ptr = 0;
    for ( int i = y-6, j = x+6 ; i < y+6 ; i++,j-- ){
        checkListSlash2[ptr] = board[i][j];
        ptr++;
    }
    checker(checkListSlash2);
    if ( haveWinner ) return; 
}



int solve(){
    int x = 0, y = 0;
    int times = 1;
    char gura = '\0';
    while ( scanf("%i%c%i",&y,&gura,&x) != EOF ){
        if ( !haveWinner ){
            y += 5, x += 5; //shift
            if ( times == 1 || times%2 != 0 ) board[y][x] = 1;
            else board[y][x] = 2;
            times++;
            everyTimeOut(y,x);
            if ( times >= 5 ){
                generater(y,x);
            }
        }
    }
    printf(winner == 1 ? "black\n" : "white\n");
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}