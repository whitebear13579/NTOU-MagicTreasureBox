#include <stdio.h>
#include <math.h>

//對於左界，向上取整；對於右界，向下取整

int main(){
    double a = 0, b = 0;  
    while ( scanf("%lf %lf",&a,&b) != EOF ){
        int l = ceil(200 * log(a)), r = floor(200 * log(b));
        printf("%i\n",r-l+1);
    }
    return 0;
}