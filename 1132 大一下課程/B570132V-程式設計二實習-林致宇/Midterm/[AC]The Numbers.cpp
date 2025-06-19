#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;
 
//usage cin >> n -> n = nextint()

const int N = 7;
int t = 0;

/*
    https://etutor2.itsa.org.tw/mod/teachingzone/problem_view.php?id=36655
    Problem Statement:
    The Numbers (20%)
    Write a program to report the number of times a number N appears in another number M. The number N is between 10 and 99, and the number M is between 1000000 and 9999999, inclusively.

    Input File Format: 
    There are two numbers in the input -- N followed by M.

    Output Format: 
    The output has only one number, namely the number of times N appears in M.

    Requirement:
    1. N and M should be integers.
    2. You cannot convert N and M to strings. 

    Example:
    Input 1:
    90 9090999
    Output 1:
    2

    Input 2:
    11 1110111
    Output 2:
    4
*/

/*
Requirement:
1. N and M should be integers.
2. You cannot convert N and M to strings. 
*/

inline signed solve(){
    int tar1 = 0, tar2 = 0;
    vi numberSet(N,0);
    scanf("%1i%1i",&tar1,&tar2);
    scanf("%1i%1i%1i%1i%1i%1i%1i",&numberSet[0],&numberSet[1],&numberSet[2],&numberSet[3],&numberSet[4],&numberSet[5],&numberSet[6]);

    for ( int i = 0 ; i < N-1 ; i++ ){
        if ( numberSet[i] == tar1 && numberSet[i+1] == tar2 ) t++;
    }
    cout << t << "\n";
    return 0;
}

signed main(){
    whitebear;
    /*int t = 0;
     t = nextint();
     while(t--)*/
         solve();
    return 0;
}
