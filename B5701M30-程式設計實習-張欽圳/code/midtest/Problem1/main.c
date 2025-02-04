#include<stdio.h>
#include<stdlib.h>

unsigned long long _u = 9837266468374616373LLU, _v = 4101842887655102021LLU, _w = 1;
unsigned long long secret()
{
    unsigned long long  x;
    _u  = _u * 2862933555777941757LLU + 7046029254386353087LLU;
    _v ^= _v >> 17; _v ^= _v << 31; _v ^= _v >> 8;
    _w  = 4294957665U * (_w & 0xffffffff) + (_w >> 32);
    x   = _u ^ (_u << 21); x ^= x >> 35; x ^= x << 4;
    return (x + _v) ^ _w;
}

void set(unsigned long long s) {
     unsigned long long t = _v;
    _w = _v;
    _v = s ^ _v;
    _u = t;
    return;
}

int main()
{
    int gura = 0;
    unsigned i,k;
    unsigned x;
    unsigned long long r; // notice that r is unsigned long long
    
    set(2024);

    r = secret();
    /* check point #1  */

    r = secret();
    /* check point #2 */

    for(i = 0;i<10000;++i) {
        gura++; /* check point #3 when i == 2024 */
        r = secret()+secret();
        gura++; /* check point #4 when i == 2024 */
    }

    return 0;
}