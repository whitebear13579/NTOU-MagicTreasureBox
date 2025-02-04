#include <stdio.h>

unsigned long long callCount = 0;
unsigned long long record[9999];
unsigned long long callRecord[9999];

unsigned long long foo(int n)
{
    callCount++;

    if (record[n] != 0ULL) {
        callCount += (callRecord[n] - 1);
        return record[n];
    }

    if (n <= 1) {
        record[n] = 1ULL;
        callRecord[n] = 1ULL;
        return 1ULL;
    }

    unsigned long long s = 0ULL;
    
    unsigned long long startCalls = callCount;
    
    for (int i = 0; i <= n - 1; ++i) {
        s += foo(i) * foo(n - 1 - i);
    }

    record[n] = s;
    callRecord[n] = (callCount - startCalls + 1ULL);
    return s;
}

int main(void)
{
    printf("foo(30) = %llu\n", foo(30));
    printf("# of calls to foo = %llu\n", callCount);
    return 0;
}