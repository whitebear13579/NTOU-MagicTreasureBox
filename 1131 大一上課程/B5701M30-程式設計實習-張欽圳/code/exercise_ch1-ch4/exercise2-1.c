#include <stdio.h>
#include <stdlib.h>

int main(){
    int a = 0, b = 0;
    scanf("%i %i",&a,&b);
    printf("sum:%i\nproduct:%i\ndifference:%i\n",a+b, a*b, abs(a-b) );
    if( b == 0 ){
        printf("quotient:invalid\nremainder:invalid\n");
    }else {
        int quotient = a/b;
        printf( "quotient:%i\nremainder:%i\n", quotient, a-(quotient*b) );
    }
    return 0;
}