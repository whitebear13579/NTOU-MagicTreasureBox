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

int main( int argc,char* argv[] ){
    char fileBuffer[1000];
    FILE* fp;
    if ( argc > 1 ){
        // via cmd
        fp = fopen(argv[1],"rb");
        if ( fp == NULL ){
            printf("%s not found\n", argv[1]);
            return 0;
        }
    }else{
        scanf("%s",fileBuffer);
        fp = fopen(fileBuffer,"rb");
        if ( fp == NULL ){
            printf("%s not found\n", fileBuffer);
            return 0;
        }
    }
    unsigned char data[16];
    unsigned offset = 0;
    int stop = 0;
    while( (stop = fread(data,1,16,fp)) > 0 ){
        printf("%08X:",offset);
        int i = 0;
        for ( i = 0 ; i < stop ; i++ ){
            printf(" %02X",data[i]);
        }
        for ( ; i < 16 ; i++ ) printf("   ");
        printf(" ");
        offset += stop;
        for ( int i = 0 ; i < stop ; i++ ){
            printf("%c",(isprint(data[i]) ? data[i] : '.'));
        }
        printf("\n");      
    }
    fclose(fp);
    return 0;
}