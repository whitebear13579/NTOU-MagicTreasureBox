#include<iostream>
using namespace std;

void onOKClick() {
    cout << "OK" << endl;
}

void onCancelClick() {
    cout << "Cancel" << endl;
}

void setOnClick(void (* pfunc)()) {
    pfunc();
}

int main() {
    setOnClick(onOKClick);
}






