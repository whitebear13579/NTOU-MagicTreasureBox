#include <stdio.h>
#define Y "diamond:"

int main(){
    int n = 0;
    while( scanf("%i",&n) != EOF ){
        if ( n == 1 ){
            printf(Y);
            printf("1\n*\n");
        }else{
            printf(Y);
            printf("%i\n",n);
            //上半部分
            int _time = (n/2);
            for ( int i = 0 ; i < (n/2)+1 ; i++ ){
                for ( int j = 0; j < _time ; j++ ) printf("-");
                for ( int j = 0 ; j < n-(_time*2) ; j++ ) ( j == 0 || j == (n-(_time*2))-1  ? printf("*") : printf("-") );
                for ( int j = 0; j < _time ; j++ ) printf("-");
                _time--;
                printf("\n");
            }
            //下半部分
            int _htime = 1;
            for ( int i = 0 ; i < (n/2) ; i++ ){
                for ( int j = 0; j < _htime ; j++ ) printf("-");
                for ( int j = 0 ; j < n-(_htime*2) ; j++ ) ( j == 0 || j == (n-(_htime*2))-1  ? printf("*") : printf("-") );
                for ( int j = 0; j < _htime ; j++ ) printf("-");
                _htime++;
                printf("\n");
            }
        }
    }
}