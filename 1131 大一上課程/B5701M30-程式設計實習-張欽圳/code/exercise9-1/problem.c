// not allow P1{
#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
/* 

   This program reads three integer numbers and outputs the largest one.  
   You should correct the following two functions: 
               largest_version1 and largest_version2 
   to make the integer pointer largestPtr correctly  point to the largest among num1, num2, and num3. 

*/
// not allow P1 END.}

void largest_version1(int** largest, int *x, int *y, int *z)
{
     if(*x>=*y) {
         *largest = (*x>=*z)? x : z;
     } else {
         *largest = (*y>=*z) ? y : z;
     }

     x = y = z; /* not allow to change. line 22*/
     return;
}
int* largest_version2(int* x, int* y, int* z)
{
       if(*x>=*y) {
         return (*x>=*z)? x : z;
       } else {
         return (*y>=*z) ? y : z;
       } 
}

// not allow P2{
void foo(int a,int b,int c)

{

      return;

}
int main()
{
   int num1,num2,num3; 
   int* largestPtr=NULL;
   scanf("%d%d%d",&num1,&num2,&num3);
   largest_version1(&largestPtr,&num1,&num2,&num3); // Called Varible ALLOW to change. line 47

   foo(num3,num2,num1);
   if (largestPtr==&num1 || largestPtr==&num2|| largestPtr==&num3){
         printf("The largest number is %d.\n", *largestPtr);  
   }
   largestPtr = largest_version2(&num1,&num2,&num3); // Called Varible ALLOW to change. line 53
   foo(num3,num2,num1);
   if (largestPtr==&num1 || largestPtr==&num2|| largestPtr==&num3){
         printf("The largest number is %d.\n", *largestPtr);  
   }
   return 0; 
} 
// not allow P2 END.}