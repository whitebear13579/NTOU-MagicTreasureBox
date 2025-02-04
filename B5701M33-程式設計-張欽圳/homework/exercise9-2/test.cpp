#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
#define pii pair<int,int>
#define f first
#define s second
using namespace std;
std::ofstream ofs;
 
//usage cin >> n -> n = nextint()

signed main(){
    int m = 0 , n = 0;
    cin >> m >> n;
    if ( m == 6 and n == 3 ) ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise9-2\\1.in"); 
    else if ( m == 6 and n == 4 ) ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise9-2\\3.in"); 
    else ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise9-2\\2.in");

    if ( !ofs.is_open() ){
        cerr << "Failed to open the external file.\n";
    }

    ofs << m << " " << n << "\n";
    int tp = 0;
    while( cin >> tp ){
        ofs << tp << " ";
    }ofs << "\n";
    cout << "Process Done.\n";
    return 0;
}