#include <stdio.h>
#include <math.h>

int main(){
    int u = 0, v = 0, a = 0 , b = 0, c = 0;
    double distane = 0;
    scanf("%i %i %i %i %i",&u ,&v,&a ,&b ,&c);
    distane =  fabs(a*u+b*v+c)/sqrt(a*a+b*b);
    printf("%.2f\n",distane);
}