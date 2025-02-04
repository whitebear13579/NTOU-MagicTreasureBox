#include <stdio.h>
#include <math.h>

int main(){
    double x = 0, t = 0;
    while( scanf("%lf %lf",&t,&x) != EOF ){
        double ans = 0;
        for ( double k = 0 ; k <= t ; k++ ){
            ans += ((pow(-1.0,k))*pow(x,1.0+2.0*k))/(1.0+2.0*k);
        }
        printf("%.5f\n",ans);
    }
    return 0;
}
