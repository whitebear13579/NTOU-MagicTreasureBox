#include <stdio.h>
#include <string.h>

int main(){
    int n = 0;
    scanf("%i",&n);
    
    for ( int i = 0 ; i < n ; i++ ){
        int hh = 0, mm = 0, ss = 0;
        int h1 = 0, h2 = 0;
        scanf("%1i%1i:%i:%i",&h1,&h2,&mm,&ss);
        hh = (h1*10)+h2;
        if ( hh == 0 || hh == 12 ){
            ( hh == 0 ? printf("12:%02i:%02i a.m.\n",mm,ss) : printf("12:%02i:%02i p.m.\n",mm,ss) );
        }else{
            ( hh > 12 ? printf("%02i:%02i:%02i p.m.\n",hh-12,mm,ss) : printf("%02i:%02i:%02i a.m.\n",hh,mm,ss) );
        }
    }
    return 0;
}