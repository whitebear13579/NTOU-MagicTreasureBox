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
    int nr = 0, ng = 0;
    cin >> nr >> ng;
    if ( nr == 1 and ng == 0 ){
        ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise10-1\\1.in");
        if ( !ofs.is_open() ) cerr << "Failed to open the external file.\n";
    }else if ( nr == 0 and ng == 10 ){
        ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise10-1\\3.in");
        if ( !ofs.is_open() ) cerr << "Failed to open the external file.\n";
    }else{
        ofs.open("D:\\Project\\C++\\NTOU_1B_CodeCourse\\code_FRI\\exercise10-1\\2.in");
        if ( !ofs.is_open() ) cerr << "Failed to open the external file.\n";
    }
    ofs << nr <<" "<< ng;
    string s;
    while(getline(cin,s)){
        ofs << s << "\n";
    }
    cout << "Process Done.\n";
    ofs.close();
    return 0;
}