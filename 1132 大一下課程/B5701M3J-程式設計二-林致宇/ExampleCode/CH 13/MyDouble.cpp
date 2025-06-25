#include<iostream>
using namespace std;

class Double {
public:
    double value;
    Double operator=(const double d) {
        this->value = d;
        return *this;
    }
    operator int() {
        int tmp = (int)value;
        if ((int)(value*10.0)%10 >= 5) {
            tmp++;
        }
        return tmp;
    }
    
};

int main(int argc, const char * argv[]) {
    Double d;
    d = 5.7;
    cout << (int)d << endl;

}