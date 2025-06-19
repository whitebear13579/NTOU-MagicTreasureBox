#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#define int long long int

int dpoly(int n,int x);
int a[500];

int poww(int x, int y) {
    if(y == 0) return 1;
    if(y&1) return x*poww(x*x, y/2);
    else return poww(x*x, y/2);
}

signed main()
{
  int n;
  int i;
  int x;
  int ans = 0;
  char c;
  while (scanf("%lld", &x) != EOF) {
      n = -1;
      do {
           n++;
           scanf("%lld%c", &a[n], &c);
     } while (c == ' ');

    ans = dpoly(n, x);

    printf("%lld\n", ans);
  }
  return 0;
}

int dpoly(int n,int x){
    int gura = 0;
    for ( int i = 0 ; i <= n ; i++ )  {
        gura += a[i]*(n-i)*poww(x,n-i-1);
    }
    return gura;
}