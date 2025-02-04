#include<stdio.h>
#define int long long int
int a[500];

int x, n;

int pw(int x, int y) {
    if(y == 0)
        return 1;
    if(y&1)
        return x*pw(x*x, y/2);
    else
        return pw(x*x, y/2);
}

signed main()
{
  int i;
  int ans = 0;
  char c;

  while (scanf("%lld", &x) != EOF) {
      n = -1, ans = 0;
      do {
            n++;
           scanf("%lld%c", &a[n], &c);
     } while (c == ' ');

    // calculate the answer
    // ans= dpoly(.....
    for(int i = 0; i <= n; i ++){
        ans += ( pw(x, n-i-1) * a[i] * (n-i) );
    }

    printf("%lld\n", ans);
  }
  return 0;
}