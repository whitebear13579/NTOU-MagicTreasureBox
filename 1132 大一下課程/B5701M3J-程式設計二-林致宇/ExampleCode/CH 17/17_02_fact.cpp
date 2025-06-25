#include<iostream>
using namespace std;

double fact(int n) {
   if (n > 170 || n < 0) {
      throw n;
   }
   double total = 1;
   for (int i = 1; i <= n; i++) {
      total *= i;
   }
   return total;
}

int main() {
   int x = 5, y = -2;

   try {
      cout << "C(" << x << "," << y << ") = "
         << fact(x)/(fact(x-y)*fact(y)) << endl;         
   } catch (int i) {
      cout << "Exception: " << i << endl;
   }
}







