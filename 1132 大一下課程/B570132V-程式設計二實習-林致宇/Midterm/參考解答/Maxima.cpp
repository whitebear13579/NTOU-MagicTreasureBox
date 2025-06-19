#include<iostream>    
#include<vector>
using namespace std;

class Point {   
public:
    int x;   
    int y;   
};


int main() {
    
    vector<Point> points;
    vector<Point> maxima;
    
    int n;
    cin >> n;
    
    for (int i = 0 ; i < n; i++) {
        Point p;
        cin >> p.x;
        cin >> p.y;
        points.push_back(p);
    }
    
    
    for (int i = 0; i < points.size(); i++) {
        
        int m = 1; // true
        
        for (int j = 0; j < points.size(); j++) {
            if (i == j) continue;
            
            if (points[j].x > points[i].x && points[j].y > points[i].y) {
                m = 0; // false
                break;
            }
        }
        if (m == 1) {
            maxima.push_back(points[i]);
        }
    }
    
    
    for (int i = 0; i < maxima.size(); i++) {
        
        int minIndex = i;
        for (int j = i+1; j < maxima.size(); j++) {
            if (maxima[j].x < maxima[i].x) {
                minIndex = j;
            } else if (maxima[j].x == maxima[i].x) {
                if (maxima[j].y < maxima[i].y) {
                    minIndex = j;
                }
            }
        }
        Point tmp;
        tmp.x = maxima[i].x;
        tmp.y = maxima[i].y;
        maxima[i].x = maxima[minIndex].x;
        maxima[i].y = maxima[minIndex].y;
        maxima[minIndex].x = tmp.x;
        maxima[minIndex].y = tmp.y;
    }
    
    for (int i = 0; i < maxima.size(); i++) {
        cout << maxima[i].x << " " << maxima[i].y;
        if (i == maxima.size()-1) {
            cout << endl;
        } else {
            cout << " ";
        }
    }
    
}