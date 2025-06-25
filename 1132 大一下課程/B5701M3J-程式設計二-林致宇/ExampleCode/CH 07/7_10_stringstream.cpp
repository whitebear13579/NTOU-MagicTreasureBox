#include<iostream>
#include<sstream>
#include <string>
using namespace std;

int main() {

	int iarr[100];
	string line, token;

	getline(cin, line);
	stringstream ss(line);

	int count = 0;
    while (getline(ss, token, ' ')) {
		iarr[count++] = stoi(token);
    }
	for (int i = count-1; i > 0; i--) {
		cout << iarr[i] << ' ';
	}
	cout << iarr[0] << '\n';
}
