#include <stdio.h>
#include <math.h>
#define MAX 100000

//key: Uk - Lk = 4/4n+5

int main(){
    double gura = 0;
    int lucky = 0;
    while ( scanf("%lf",&gura) != EOF ){
        for ( int i = 0 ; i < MAX ; i++ ){
            if ( 4.0/(4.0*i+5.0) < gura ){
                lucky = i;
                break;
            }
        }
        printf("%i\n",lucky);
    }
    return 0;
}
