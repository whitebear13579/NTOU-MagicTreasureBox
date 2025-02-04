#include <stdio.h>
#include <stdlib.h>
#define N "invalid\n"
#define Y "valid\n"

const int Debugging = 0;

void outputTesting( int t1, int t2[], int ptr ){
    printf("__DEBUGGING__T1: %i\n",t1);
    printf("__DEBUGGING__T1: %i\n",t1);
    printf("__DEBUGGING__ARRAY:");
    for ( int i = 0 ; i < ptr ; i++ ){
        printf("%i ",t2[i]);
    }
    printf("\n");
}

// Etutor2 使用 bool-type 的變數會有問題
// 避免使用 stdbool.h 與 使用 bool type 的變數

int main(){
    char happy = '\0';
    int birthday = 0;
    while ( scanf("%i%c",&birthday,&happy)!= EOF ){
        int ptr = 0;
        int gura[999] = { 0 };
        if ( happy == '\n' || happy == '\r' ){
            printf(N);
            continue;
        }
        gura[ptr] = birthday;
        ptr++;
        while ( scanf("%i%c",&birthday,&happy)){
            gura[ptr] = birthday;
            ptr++;
            if ( happy == '\n' || happy == '\r' )break;
            
        }
        if ( Debugging == 1 ) outputTesting(birthday,gura,ptr);
        //bool Passed = 1;
        int Passed = 1;
        for( int i = 0, j = ptr-1 ; i < (ptr/2) ; i++,j-- ){
            if ( abs(gura[i]) != gura[j] ){
                Passed = 0;
                break;
            }
        }
        ( Passed ? printf(Y) : printf(N) );        
    }
    return 0;
}