#include<iostream>
using namespace std;
 
int main()
{
     /*Enter the value of x: if x>0, program execute normally:
        if x<0: the program will catch the error and display the error:*/
   int x;
   cout<<" Enter the value of x: ";
   cin>>x;
   cout<<" Before try"<<endl;
   try {
      cout<<" Inside try"<<endl;
      if(x < 0) { //check the code here:
         throw x; //if x<0, throw x:
        //after throw statement: nothing will execute in try block ever:
         cout<< "After throw"<<endl; //will not execute ever: 
      }
   } catch (int x) { //if try throw any exception, catch it:
     //if the program will catch exception: then the code inside this block will execute:
     cout<<" Exception Caught: " << x <<endl; 
     //handle the exception here,
   }
 
   cout<<" After catch" <<endl; 
   return 0;
}






