#include<iostream>
#include<cstring>
using namespace std;

int main(int argc, const char * argv[]) {

    int array[2][3] = {{1, 2, 3}, {4, 5}};

    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 3; j++) {
            cout << array[i][j] << "\t";
        }
        cout << "\n";
    }
}
