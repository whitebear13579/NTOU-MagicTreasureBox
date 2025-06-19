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
    ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise11-1\\2.in");
    
    if ( !ofs.is_open() ){
        cerr << "Failed to open the external file.\n";
    }//else ofs << "std::ofstream open external file successfully.\n-----------------------------------------------\n\n";

    string s;
    while ( getline(cin,s) ) ofs << s << "\n"; 
    cout << "Process Done.\n";
    ofs.close();
    return 0;
}