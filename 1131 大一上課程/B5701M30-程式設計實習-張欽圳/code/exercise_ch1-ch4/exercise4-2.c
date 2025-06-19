#include <stdio.h>
#include <math.h>

char gura[9] = { 0 };

int main(){
    char uwu;
    int ame = 0;
    while ( scanf(" %1c",&uwu) != EOF ){
        if ( uwu == '\n' )continue;
        int oneExisted = 0;
        int ptr = 0;  
        if ( uwu != '0' ){
            oneExisted = 1;
            gura[ptr] = uwu;
            ptr++;
        }else if ( oneExisted == 1 ){
            gura[ptr] = uwu;
            ptr++;
        }
        ame = 1;
        while ( scanf("%1c",&uwu)){
            if ( uwu < '0' || uwu > '9' ) break;
            if ( uwu == '1' ){
                oneExisted = 1;
                gura[ptr] = uwu;
                ptr++;
            }else if ( oneExisted == 1 ){
                gura[ptr] = uwu;
                ptr++;
            }
        }
        if ( oneExisted == 0 ){
            printf("0\n");
        }else{
            if (ptr > 3){
                int ok = ptr%3;
                int oao = 0;
                for ( int i = 0 ; i < ok ; i++ ){
                    printf("%1c",gura[i]);
                    oao = 1;
                }
                if ( oao == 1 ) printf(",");
                
                int pptr = ok;
                int cnt = 0;
                for ( int i = pptr ; i < ptr ; i++ ){
                    if ( cnt == 3 ) printf(","), cnt = 0;
                    printf("%1c",gura[i]);
                    cnt++;
                }
                printf("\n");
            }else if (ptr>0) {
            
                for ( int i = 0 ; i < ptr ; i++ ){
                    printf("%1c",gura[i]);
                }
                printf("\n");
            }
        }
    }
    return 0;
}