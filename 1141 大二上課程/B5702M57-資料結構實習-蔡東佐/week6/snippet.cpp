#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;

#define MAX_STACK_SIZE 100 /*maximum stack size*/
typedef struct {
    int row;
    int col;
    int dir;
} element;
element stack[MAX_STACK_SIZE];

void path (void){
    int i, row, col, next_row, next_col, dir, found = FALSE;
    element position;
    mark[1][1] = 1; top =0;
    stack[0].row = 1; stack[0].col = 1; stack[0].dir = 0;

    while (top > -1 && !found) {
        position = delete(&top);
        row = position.row; col = position.col;
        dir = position.dir;
        while (dir < 8 && !found) {
            /*move in direction dir */
            next_row = row + move[dir].vert;
            next_col = col + move[dir].horiz;
            if (next_row==EXIT_ROW && next_col==EXIT_COL)
                found = TRUE;
            else if ( maze[next_row][next_col]==0 &&
                mark[next_row][next_col]==0){
                mark[next_row][next_col] = 1;
                position.row = row; position.col = col;
                position.dir = ++dir;
                add(&top, position);
                row = next_row; col = next_col; dir = 0;
            }
            else ++dir;
        }
    }
    if (found) {
        printf(“The path is :\n”);
        printf(“row col\n”);
        for (i = 0; i <= top; i++)
            printf(“ %2d%5d”, stack[i].row, stack[i].col);
        printf(“%2d%5d\n”, row, col);
        printf(“%2d%5d\n”, EXIT_ROW, EXIT_COL);
    }
    else printf(“The maze does not have a path\n”);
}


 
inline int nextint() {
    int x = 0, c = getchar(), neg = false;
    while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
    if(c == '-') neg = true, c = getchar();
    while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
    if(neg) x = -x;
    return x; // returns 0 if EOF
}

inline signed solve(){
    
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