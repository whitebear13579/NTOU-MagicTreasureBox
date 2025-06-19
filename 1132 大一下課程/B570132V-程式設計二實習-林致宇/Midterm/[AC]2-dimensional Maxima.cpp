#include <iostream>
#include <vector>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

/*
    https://etutor2.itsa.org.tw/mod/teachingzone/problem_view.php?id=36654
    Problem Statement:
    2-dimensional Maxima (20%)
    https://lincyu075.github.io/2Dmaxima.png
    
    Def: A point (x1, y1) dominates (x2, y2) if x1 > x2 and y1 > y2.  A point is called a maxima if no other point dominates it.
    Problem: Given a set of points P = {p1, p2,..., pn} in 2-dimensional space, each represented by its x and y integer coordinates, output the set of the maximal points of P, that is, those points pi, such that pi is not dominated by any other point of P.
    An example is shown above, where (7, 13), (12, 12), (14,10), and (15, 7) are maximal points: (Note that when you output the maximal points, the point with smaller x should be outputted first. If two points have the same value of x, then the point with smaller y should be outputted first.)
    
    1st line: How many nodes you have
    2nd line: nodes (x1 y1 x2 y2 ... xn yn)

    Format Requirement:
    (1) You should add a structure called Point as follows (If you use pair class provided by STL, you will only get partial credit.):
    struct Point {   
        int x;   
        int y;   
    };
    (2) You can only include two header files: #include<iostream> and #include<vector> (If you don't follow this requirement, you will only get partial credit.).
    
    Example:(Please refer to the above figure.
    Input 1:
    12
    7 7 11 5 7 13 15 7 9 10 14 10 4 11 2 5 4 4 12 12 5 1 13 3
    Output 1:
    7 13 12 12 14 10 15 7
*/ 

//usage cin >> n -> n = nextint()
 
inline char readchar() {
    const int S = 1<<20; // buffer size
    static char buf[S], *p = buf, *q = buf;
    if(p == q && (q = (p=buf)+fread(buf,1,S,stdin)) == buf) return EOF;
    return *p++;
}
 
inline int nextint() {
    int x = 0, c = getchar(), neg = false;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = true, c = getchar();
    while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

struct Point{
    int x;
    int y;
};

inline signed solve(){
    int n = 0;
    n = nextint();
    vector<Point> nodes(n);
    vector<Point> ans;
    int dx = 0, dy = 0;
    for ( int i = 0 ; i < n ; i++ ){
        dx = nextint() ,  dy = nextint();
        nodes[i].x = dx , nodes[i].y = dy;
    }

    //find the maxima
    for ( int i = 0 ; i < n ; i++ ){
        auto now = nodes[i];
        bool flag = 1;
        for ( int j = 0 ; j < n ; j++ ){
            if ( i == j ) continue;

            if ( now.x < nodes[j].x && now.y < nodes[j].y ){
                flag = 0;
                break;
            }
        }
        if ( flag ) ans.push_back(now);
    }

    //bubble sort
    for ( int i = 0 ; i < ans.size() ; i++ ){
        for ( int j = i+1 ; j < ans.size() ; j++ ){
            if ( ans[i].x > ans[j].x ){ // 先以x排
                int tpX = ans[i].x , tpY = ans[i].y;
                ans[i].x = ans[j].x , ans[i].y = ans[j].y;
                ans[j].x = tpX , ans[j].y = tpY;
            }else if ( ans[i].x == ans[j].x ){
                if ( ans[i].y > ans[j].y ){ // 以y排
                    int tpX = ans[i].x , tpY = ans[i].y;
                    ans[i].x = ans[j].x , ans[i].y = ans[j].y;
                    ans[j].x = tpX , ans[j].y = tpY;
                }
            }
        }
    }

    for ( int i = 0 ; i < ans.size() ; i++  ){
        cout << ans[i].x << " " << ans[i].y << ( i == ans.size() - 1 ? "\n" : " " );
    }
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
