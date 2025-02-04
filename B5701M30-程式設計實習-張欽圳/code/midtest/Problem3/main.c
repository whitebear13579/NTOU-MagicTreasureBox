#include<stdio.h>

// Caution! Not a ficbonnic sequence
int callTime = 0;

long long unsigned f(int n)
{
    if ( n == 10) callTime++;
    long long unsigned r = 0;
    if (n==1) {// base case
        return 1;
    }
    r = f(n-1) + f(n-1) + 1;

    return r;
}
int main()
{
    long long unsigned r = f(21);
    printf("%llu\n",r);
    printf("F(10) CallTime: %i\n",callTime);
    return 0;   
}
