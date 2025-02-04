#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>
#define Y "Yes\n"
#define N "No\n"
#define out printf("%s\n",tempRecord.msg)
 
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
struct record {
    char msg[8];
    unsigned next;
};

// formula of the position of x record is : sizeof(int) + (x-1) * sizeof(struct record)

int solve(){
    char fileBuffer[1000];
    scanf("%s",fileBuffer);
    FILE* fp = fopen(fileBuffer,"rb");
    if ( fp == NULL ){
        printf("%s not found\n", fileBuffer);
    }else{
        // 讀取檔案頭
        int fileStart = 0;
        fread(&fileStart,sizeof(int),1,fp);
        struct record tempRecord;
        fseek(fp,sizeof(int)+(fileStart-1)*sizeof(struct record),SEEK_SET);
        fread(&tempRecord.msg,sizeof(char),8,fp);
        fread(&tempRecord.next,sizeof(int),1,fp);
        out;
        while( tempRecord.next != 0 ){
            fseek(fp,sizeof(int)+(tempRecord.next-1)*sizeof(struct record),SEEK_SET);
            fread(&tempRecord.msg,sizeof(char),8,fp);
            fread(&tempRecord.next,sizeof(int),1,fp);
            out;
        }
        fclose(fp);
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