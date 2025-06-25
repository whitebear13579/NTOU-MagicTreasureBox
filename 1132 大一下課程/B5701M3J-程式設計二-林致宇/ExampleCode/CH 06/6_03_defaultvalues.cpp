#include<iostream>
#include<string>
using namespace std;

void showPersonalInfo(int id, string name="John", int age = 30) {
   cout << "ID: " << id << endl;
   cout << "Name: " << name << endl;
   cout << "Age: " << age << endl;
}

int main() {

    showPersonalInfo(1);
    showPersonalInfo(2, "Bob");
    showPersonalInfo(3, "Peter", 50);
}

