#include <stdio.h>

int main(){
    int n = 0, m = 0;
    while( scanf("%i %i",&n,&m) != EOF ){
        int next = 0;
        for ( int i = n*-1 ; i < 0 ; i = i + m ){
            printf("%i ",i);
            next = i;
        }
        printf("0 ");
        next*=-1;
        for ( int i = next ; i <= n ; i = i + m ){
            (i != n ? printf("%i ",i) : printf("%i\n",i) );
        }
    }
    return 0;
}
