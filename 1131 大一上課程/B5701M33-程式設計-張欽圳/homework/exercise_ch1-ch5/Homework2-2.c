#include <stdio.h>

int factorial( int n ){
    int result = 1;
    for ( int i = 1 ; i <= n ; i++ ){
        result *= i;
    }
    return result;
}

int main(){
    int n = 0, k = 0, m = 0;
    scanf("%i",&k);
    for ( int i = 0 ; i < k ; i++ ){
        scanf("%i %i",&n,&m);
        printf("%i\n",(factorial(n)%m));
    }
    return 0;
}