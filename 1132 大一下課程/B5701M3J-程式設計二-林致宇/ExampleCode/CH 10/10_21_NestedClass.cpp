#include<iostream>
using namespace std;

class Car {
private:
    class Driver {
    public:
        string name;
    };

    Driver driver;
public:
    int color;
    void setDriverName(string name) {
        driver.name = name;
    }
    void showDriverName() {
        cout << driver.name << endl;
    }
};

int main(int argc, const char * argv[]) {
    Car car1;
    car1.setDriverName("Bob");
    car1.showDriverName();
}




