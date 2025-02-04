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

char upperM[] = "My";
char lowerM[] = "my";
char upperTag = '@';
char lowerTag = '$';

int solve(){
    char input[1500];
    while( fgets(input,sizeof(input),stdin) != NULL ){
        int leng = strlen(input);
        char buffer[3];
        for ( int i = 0 ; i < leng ; i++ ){
            if ( (input[i] == 'M' || input[i] == 'm') && !isalpha(input[i+2]) && !isalpha(input[i-1]) && (input[i-1] != upperTag || input[i-1] != lowerTag) ) {
                buffer[0] = input[i], buffer[1] = input[i+1], buffer[2] = '\0';
                if ( !strcmp(buffer,upperM) ) input[i] = upperTag;
                if ( !strcmp(buffer,lowerM) ) input[i] = lowerTag;
            }
        }
        for ( int i = 0 ; i < leng ; i++ ){
            if ( input[i] == upperTag ) printf("Your"), i+=1;
            else if ( input[i] == lowerTag ) printf("your"), i+=1;
            else printf("%c",input[i]);
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