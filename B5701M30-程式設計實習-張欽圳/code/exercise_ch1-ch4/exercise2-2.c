#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

/*大的，找最先的 小的，找最後的  */
int main(){
    int x = 0;
    int times = 0 , sum = 0;
    int largest = -9999, indexLargest = 0, smallest = 9999, indexSmallest = 0;
    while( scanf("%d",&x) == 1 ){
        times++;
        sum += x;
        
        if ( x > largest ){
            largest = x;
            indexLargest = times;
        }else if ( x <= smallest){
            smallest = x;
            indexSmallest = times;
        }
    }
    printf("number:%i\nsum:%i\nthe index of the smallest number:%i\nthe value of the smallest number:%i\nthe index of the largest number:%i\nthe value of the largest number:%i\n",times, sum, indexSmallest-1, smallest, indexLargest-1, largest );
    return 0;
}