#include <stdio.h>

int main(){
    double weight = 0, height = 0;
    scanf("%lf %lf", &weight, &height);
    printf("%.2f\n", (weight/(height*height)));
    return 0;
}