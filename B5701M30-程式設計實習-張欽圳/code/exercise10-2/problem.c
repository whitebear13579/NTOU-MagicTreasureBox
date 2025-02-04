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

// marks: , . ' " ? !  (  )  {  }  [  ]  ;  :

bool marksChecker( char c ){
    char marks[14] = {',','.','\'','\"','?','!','(',')','{','}','[',']',';',':'};
    for ( int i = 0 ; i < 14 ; i++ ){
        if ( c == marks[i] ) return 1;
    }
    return 0;
}

int solve(){
    int totalChar = 0;
    int totalMarks = 0;
    int totalWords = 0;
    char gura;
    bool isInWord = 0;
    while (gura = getchar()){
        if ( gura == EOF ) break;
        totalChar++;
        if ( isInWord == 1 && !isalpha(gura) )isInWord = 0;
        if ( marksChecker(gura) )totalMarks++;
        if ( isalpha(gura) && isInWord == 0 ){
            isInWord = 1;
            totalWords++;
        }
    }
    printf("%i\n%i\n%i\n",totalChar,totalMarks,totalWords);
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}