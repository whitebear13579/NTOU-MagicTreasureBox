#include<iostream>
#include<ctime>

using namespace std;

int main() {

    clock_t starttime = clock();
    for (int i = 0; i < 100000000; i++);
    clock_t endtime = clock();

    cout << CLOCKS_PER_SEC << "\n";
    cout << ((double)(endtime-starttime) / CLOCKS_PER_SEC) << "秒\n";


}




