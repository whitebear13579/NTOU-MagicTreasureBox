#include <stdio.h>
#include <math.h>

int how ( int a, int b ){
    int ans = 0;
    int i = 0;
    while ( 1 ){
        double now = exp(i/200.0);
        if( now >= a && now <= b ){
            ans++;
        }else if ( now > b ){
            break;
        }
        i++;
    }
    return ans;
}

int main(){
    int a = 0, b = 0;  
    while ( scanf("%i %i",&a,&b) != EOF ){
        printf("%i\n",how(a,b));
    }
    return 0;
}