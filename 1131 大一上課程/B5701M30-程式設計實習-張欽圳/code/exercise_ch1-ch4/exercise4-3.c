#include <stdio.h>
#include <math.h>
#define Y "Jolly\n"
#define N "Not jolly\n"
#define OK 3005

int main(){
    int gura[OK] = { 0 };
    int flag[OK] = { 0 };
    int n = 0;
    char uwu;
    int OUTOK = 0;
    while ( scanf("%1i",&n) != EOF ){
        int gura[OK] = { 0 };
        int flag[OK] = { 0 };
        for ( int i = 0 ; i < n ; i++ ) scanf("%i",&gura[i]);
        for ( int i = 0 ; i < n-1 ; i++ ){
            int tp = abs(gura[i]-gura[i+1]);
            if ( tp < OK && flag[tp] == 0 ) flag[tp] = 1;
            else if ( OUTOK == 0 ){
                printf(N);
                OUTOK = 1;
                continue;
            }
        }
        for ( int i = 1 ; i < n ; i++ ){
            if ( flag[i] == 0 && OUTOK == 0 ){
                printf(N);
                OUTOK = 1;
                continue;
            }
        }
        if ( OUTOK == 0 ) printf(Y);
        OUTOK = 0;
    }
    return 0;
}