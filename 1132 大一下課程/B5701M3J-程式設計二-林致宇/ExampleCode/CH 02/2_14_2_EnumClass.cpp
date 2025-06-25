#include<iostream>
using namespace std;

int main(int argc, const char * argv[]) {

    enum class Day {Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    };

    Day yesterday {Day::Tuesday};
    Day today {Day::Wednesday};

    cout << static_cast<int>(yesterday) << " , " << static_cast<int>(today) << endl;
}
