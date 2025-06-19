#include <stdio.h>
#include <math.h>


double gura[8] = {1.0000000, 0.1000000, 0.0100000, 0.0010000, 0.0001000, 0.0000100, 0.0000010, 0.0000001};

int main(){
    double x = 0;
    while ( scanf("%lf",&x) != EOF ){
        printf("%-9s %-9s %-15s %-15s %-15s\n","    x","    h","    cos'(x)","       ND","     Error");
        printf("%-9s %-9s %-15s %-15s %-15s\n","---------","---------","----------------","----------------","----------------");
        for ( int i = 0 ; i < 8 ; i++ ){
            double output1 = 0, output2 = 0, output3 = 0;
            output1 = sin(x) * -1.0;
            output2 = (cos(x+gura[i]) - cos(x))/gura[i];
            output3 = output2 - output1;
            if ( output3 < 0 ) output3 = output3 * -1;
            printf("%9f %.7f %16.5e %16.5e %16.5e\n",x, gura[i], output1, output2, output3 );
        }
    }
}