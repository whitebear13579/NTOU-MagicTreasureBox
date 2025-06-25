#include<iostream>
#include <deque>
using namespace std;

class Node {
public:
    Node(int xv, int yv) {
         x = xv;
         y = yv; }
    int x;
    int y;
};

int main() {
    
    Node n1(1,1), n2(2,2), n3(3,3);
    Node n4(4,4), n5(5,5), n6(6,6);

    deque<Node> d {n1, n2, n3, n4};
    d.pop_back();
    d.push_front(n5);
    d.push_back(n6);
    d.pop_front();

    for (Node node : d) {
        cout << "(" << node.x << ", " << node.y << ")" << endl;
    }

    // Random Access is supported.
    cout << "(" << d[2].x << ", " << d[2].y << ")" << endl;
}
