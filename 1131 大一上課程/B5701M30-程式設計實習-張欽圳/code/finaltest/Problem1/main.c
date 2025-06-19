#include<stdio.h>
#include<stdlib.h>
#include<string.h>

unsigned long long my_rand();
void my_seed(unsigned long long s) ;

struct value_count_struct {
    unsigned n; // the number of unique elements
    int       *value; // the sorted unique values
    unsigned *count;  // the number of times that the corresponding unique value appears in the array
};

struct value_count_struct unique(int data[],int n)
{
     struct   value_count_struct vc;
     unsigned initial_size = 8;
     int i, j, low, high, mid;


     if (n <= 0) {
        vc.n     = 0;
        vc.value = NULL;
        vc.count = NULL;
        return vc;
     }

     vc.value = (int*) malloc(sizeof(int)*initial_size);
     vc.count = (unsigned*) malloc(sizeof(unsigned)*initial_size);

     vc.n        = 1;
     vc.value[0] = data[0];
     vc.count[0] = 1;

     for(i = 1; i < n; ++i) {
         high = vc.n - 1;
         low  = 0;

         while(low <= high) {
            mid = (high+low)/2;
            if (vc.value[mid] == data[i]) {
                vc.count[mid]++;
                break;
            } else if (vc.value[mid] < data[i]) {
                low = mid + 1;
            } else {
                high = mid - 1;
            }
         }

         if (low > high) {
            if (vc.n==initial_size) {
                initial_size *= 2;
                vc.value = (int*) realloc(vc.value,sizeof(int)*initial_size);
                vc.count = (unsigned*) realloc(vc.count,sizeof(unsigned)*initial_size);
            }

            if (vc.value[mid]<data[i]) {
                mid++;
            }
            for(j = vc.n; j > mid; j--) {
                vc.value[j] = vc.value[j-1];
                vc.count[j] = vc.count[j-1];
            }
            vc.value[j] = data[i];
            vc.count[j] = 1;
            vc.n++;
         }
     }

     printf("Number of integer elements dynamically allocated for vc.value: %u\n", initial_size);
     printf("Number of unsigned elements dynamically allocated for vc.count: %u\n", initial_size);

     return vc;
}

int dataMax = -1000;
int dataMin = 1000000;
int MaxCount = 0;
int MaxCountNum = 0;

#define SIZE 80000
int main()
{
    int *data;
    int i;
    struct value_count_struct vc;

    my_seed(9873345);

    data = (int*) malloc(sizeof(int)*SIZE);

    for(i = 0; i < SIZE; ++i) {
        data[i] = my_rand() % 10000;
    }

    vc = unique(data,SIZE);

    for(i = 0; i < SIZE; ++i) {
        if ( data[i] < dataMin ) dataMin = data[i];
    }

    for(i = 0; i < SIZE; ++i) {
        if ( data[i] > dataMax ) dataMax = data[i];
    }

    for(i = 0; i < vc.n; ++i) {
        if (vc.count[i] > MaxCount) {
            MaxCount = vc.count[i];
            MaxCountNum = 1;
        } else if (vc.count[i] == MaxCount) {
            MaxCountNum++;
        }
    }
    
    printf("Max:%d\nMin:%d\n",dataMax,dataMin);
    printf("Max Count: %d\n",MaxCount);
    printf("Max Count Num: %d\n",MaxCountNum);
    printf("# of unique elements:%d\n",vc.n);

    return 0;
}

unsigned long long _u = 9837266468374616373ULL, _v = 4101842887655102021ULL, _w = 1;

unsigned long long my_rand()
{
    unsigned long long  x;
    _u  = _u * 2862933555777941757ULL + 7046029254386353087ULL;
    _v ^= _v >> 17; _v ^= _v << 31; _v ^= _v >> 8;
    _w  = 4294957665U * (_w & 0xffffffff) + (_w >> 32);
    x   = _u ^ (_u << 21); x ^= x >> 35; x ^= x << 4;
    return (x + _v) ^ _w;
}

void my_seed(unsigned long long s)
{
     unsigned long long t = _v;
    _w = _v;
    _v = s ^ _v;
    _u = t;
    return;
}