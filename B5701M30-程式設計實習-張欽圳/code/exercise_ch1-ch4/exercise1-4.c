#include<stdio.h>
int main()
{
    int n, i;

    for(n = 0, i = 0; i < 100; ++i) {
         n = i*i+i+1;
         printf("%d %d\n",i,n);
    }

    return 0;
}