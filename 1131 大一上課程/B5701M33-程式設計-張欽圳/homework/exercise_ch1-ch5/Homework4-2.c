#include <stdio.h>
#include <math.h>

int main(){
    int n = 0;
    while ( scanf("%i ",&n) != EOF ){
        int x[9] = { 0 };
        int y[9] = { 0 };
        for ( int i = 0 ; i < n ; i++ ) scanf("%1i",&x[i]);
        char nope;
        scanf("%c",&nope);
        int ans = 0;
        for ( int i = 0 ; i < n ; i++ ) scanf("%1i",&y[i]);
        for ( int i = 0 ; i < n ; i++ ){
            int tp = 0;
            if ( x[i] == y[i] ) continue;
            tp = abs(y[i]-x[i]);
            if ( tp > 5 ) tp = 10 - tp;
            ans += tp;
        }
        printf("%i\n",ans);
    }
}