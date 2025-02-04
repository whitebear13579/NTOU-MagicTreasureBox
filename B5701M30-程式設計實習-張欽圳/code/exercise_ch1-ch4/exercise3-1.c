#include <stdio.h>

int main(){
    double Weight = 0, Height = 0, BMI = 0;

    while ( scanf("%lf %lf",&Weight,&Height) != EOF ){
        BMI = Weight  / (Height*Height/10000);
        if ( BMI < 18.5 ){
            printf("BMI:%.2f Category:underweight\n",BMI);
        }else if ( BMI >= 18.5 && BMI < 24 ){
            printf("BMI:%.2f Category:normal\n",BMI);
        }else if ( BMI >= 24 && BMI < 27 ){
            printf("BMI:%.2f Category:overweight\n",BMI);
        }else if ( BMI >= 27 && BMI < 30 ){
            printf("BMI:%.2f Category:obese class I\n",BMI);
        }else if ( BMI >= 30 && BMI < 35 ){
            printf("BMI:%.2f Category:obese class II\n",BMI);
        }else{
            printf("BMI:%.2f Category:obese class III\n",BMI);
        }
    }
    return 0;
}