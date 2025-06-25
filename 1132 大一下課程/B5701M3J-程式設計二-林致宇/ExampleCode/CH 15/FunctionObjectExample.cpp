#include<iostream>
#include <vector>
#include <algorithm>
using namespace std;

class Point {
public:
    Point(int a, int b) {x=a; y=b;}
    int x;
    int y;
};

bool f(Point p1, Point p2) {
    if (p1.x < p2.x) {
        return true;
    } else if (p1.x == p2.x && p1.y < p2.y) {
        return true;
    }
    return false;
}

class CompPoint {
public:
    bool operator()(Point p1, Point p2) {
        if (p1.x < p2.x) {
            return true;
        } else if (p1.x == p2.x && p1.y < p2.y) {
            return true;
        }
        return false;    
    }
};


int main() {
    
    vector<Point> points = {Point(8,6), Point(5,7), Point(5,3)};
    
    CompPoint cp;
  
    sort(points.begin(), points.end(), cp);  
    
    for (Point p: points) {
        cout << p.x << ", " << p.y << endl;
    }
    
}