#include<stdio.h>
#include<stdlib.h>

int *read_array(int n)
{
  int *a = (int*)malloc((n+1)*sizeof(int));
  int *b;
  b = a + n;

  while(b>a) {
    b--;
    scanf("%d",b);
  }
  return a;
}

int main()
{
  int n,i;
  int *a1, *a2;

  scanf("%d",&n);
 
  a1 = read_array(n);
  a2 = read_array(n);

  for(i = 0; i < n; ++i) {
    printf("%d %d\n",a1[i],a2[i]);
  }

  return 0;
}