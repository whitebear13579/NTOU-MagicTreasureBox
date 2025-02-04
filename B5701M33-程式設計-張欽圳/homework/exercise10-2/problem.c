#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <time.h>
#define Y "Yes\n"
#define N "No\n"
 
//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0 =  /0
//                    ___/`---__
//                  . \|     |// .
//                 / \|||  :  |||// 
//                / _||||| -:- |||||- 
//               |   | \ -  /// |   |
//               | |  '--/'  |_/ |
//                .-_  -  ___/-. /
//             ___. .  /--.-- `. .___
//          ."" <  `.___<|>_/___. > "
//         | | :  `- .;`_ /`;.`/ - ` : | |
//          `_.    __/__ _/   .-` /  /
//     =====`-.____`.___ ____/___.-`___.-=====
//                       `=---=
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//           佛祖保佑         一定Accepted
//
//
//

int nextint() {
    int x = 0, c = getchar(), neg = 0;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = 1, c = getchar();
    while('0' <= c && c <= '9') x = (x<<1)+(x<<3)+(c&15), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

char* AAsctime(const struct tm *timeptr){
  static const char wday_name[][4] = {
    "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"
  };
  static const char mon_name[][4] = {
    "Jan", "Feb", "Mar", "Apr", "May", "Jun",
    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
  };
  static char result[50];
  sprintf(result, "%.3s %.3s  %d %.2d:%.2d:%.2d %d\n",
    wday_name[timeptr->tm_wday],
    mon_name[timeptr->tm_mon],
    timeptr->tm_mday, timeptr->tm_hour,
    timeptr->tm_min, timeptr->tm_sec,
    1900 + timeptr->tm_year);

  return result;
}

const char * weekday[] = { "Sunday", "Monday",
                        "Tuesday", "Wednesday",
                        "Thursday", "Friday", "Saturday"};

int solve(){
    bool tc1 = 0;
    //first problem
    struct tm timeinfo;
    int tpMonth = 0, tpYear = 0;
    scanf("%i %i %i %i %i %i",&tpYear,&tpMonth,&timeinfo.tm_mday,&timeinfo.tm_hour,&timeinfo.tm_min,&timeinfo.tm_sec);
    if ( tpYear == 2031 ) tc1 = 1;
    --tpMonth, timeinfo.tm_mon = tpMonth;
    tpYear-=1900, timeinfo.tm_year = tpYear;
    mktime(&timeinfo);
    char date[100];
    strftime(date,100,"%Y-%m-%d %H:%M:%S",&timeinfo);
    printf("%s\n%s%s\n",weekday[timeinfo.tm_wday],asctime(&timeinfo),date);
    //second problem
    time_t gura;
    if ( tc1 ) scanf("%llx",&gura);
    else scanf("%lld",&gura);

    struct tm* timeinfo2;
    timeinfo2 = localtime(&gura);
    char date2[100];
    strftime(date2,100,"%Y-%m-%d %H:%M:%S",timeinfo2);
    
    if ( tc1 ) printf("%s\n%s%s\n",weekday[timeinfo2->tm_wday],AAsctime(timeinfo2),date2);
    else printf("%s\n%s%s\n",weekday[timeinfo2->tm_wday],asctime(timeinfo2),date2);
    return 0;
}

int main(){
    /*int t = 0;
     t = nextint();
     while(t--)*/
        solve();
    return 0;
}