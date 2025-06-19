#include<stdio.h>
void score(const char p[], const char y[], int n, double*sensitivity, double* specificity);
int main()
{
     char p[1000];
     char y[1000];
     int a,b,i,n;
     double sensitivity,specificity;
     n=0;
     while(scanf("%d%d",&a,&b)!=EOF) {
             p[n]=a;
             y[n]=b;
             n++;
     }
     score(p, y, n,&sensitivity,&specificity);\
     printf("%.2lf\n",sensitivity);    
     printf("%.2lf\n",specificity);    
     return 0;
}

//function
void score(const char p[],const char y[],int n,double*sensitivity,double*specificity){
    double TP = 0, FP = 0, FN = 0, TN = 0;
    for ( int i = 0 ; i < n ; i++ ){
        if ( y[i] == 1 && p[i] == 1 ) ++TP;
        else if ( y[i] == 1 && p[i] == 0 ) ++FN;
        else if ( y[i] == 0 && p[i] == 1 ) ++FP;
        else if ( y[i] == 0 && p[i] == 0 ) ++ TN;
    }   
    *sensitivity = (TP/(TP+FN));
    *specificity = (TN/(TN+FP));
}