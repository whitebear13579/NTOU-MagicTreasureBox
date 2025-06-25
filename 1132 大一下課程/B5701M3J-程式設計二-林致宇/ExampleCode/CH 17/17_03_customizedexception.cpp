#include<iostream>
using namespace std;

class BufferFull: public exception {};

class IntBuffer {
private:
   int buffer[2];
public:
   int count;
   void add(int value) {
      if (count == 2) {
         throw BufferFull();
      }
      buffer[count++] = value;
   }
};

int main() {
   IntBuffer buf;
   buf.count = 0;
   try {
      buf.add(1);
      buf.add(2);
      // buf.add(3);
   } catch (BufferFull e) {
      cout << "BufferFull" << endl;
   }
}






