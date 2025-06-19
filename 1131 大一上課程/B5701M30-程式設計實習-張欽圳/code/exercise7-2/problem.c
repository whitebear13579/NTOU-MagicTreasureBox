#include<stdio.h>

int gg = 0;

int A(int m, int n) {

    if ( m == 3 && n == 2 ){
      gg = 0;
    }

    if (m == 0) return n + 1;


    if (m > 0 && n == 0) return A(m - 1, 1);
    return A(m - 1, A(m, n - 1));
}

int main()
{
  printf("%d\n", A(5, 5));
  return 0;
}