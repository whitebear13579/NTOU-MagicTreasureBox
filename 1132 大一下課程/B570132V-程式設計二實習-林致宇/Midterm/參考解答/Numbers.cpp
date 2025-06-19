#include<iostream>    
using namespace std;


int main() {

    int n, m;
    cin >> n;
    cin >> m;
    
    
    int t = 1000000;
    
    int n1 = n/10;
    int n2 = n%10;
    
    int count = 0;
    
    for (int i = 0; i < 6; i++) {
        int checkeddigit = m/t;
        int nextdigit = (m%t)/(t/10);
        
        // cout << checkeddigit << " , " << nextdigit << endl;
        if (checkeddigit == n1 && nextdigit == n2) {
            count++;
        }
        m = m%t;
        t = t/10;

    }
    
    cout << count << endl;
}