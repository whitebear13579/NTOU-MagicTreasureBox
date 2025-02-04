#include <iostream>
#include <vector>
#include <unordered_map>
#include <sstream>
#include <string>

using namespace std;

// Function to parse a comma-separated string into a vector of integers
void parseInput(const string &input, vector<int> &array) {
    stringstream ss(input);
    string token;
    while (getline(ss, token, ',')) {
        array.push_back(stoi(token));
    }
}

// Backtracking function to find all valid permutation vectors
void backtrack(const vector<int> &b, const unordered_map<int, vector<int>> &indexMap,
               vector<int> &current, vector<bool> &used, vector<vector<int>> &results, int pos) {
    if (pos == b.size()) {
        results.push_back(current);
        return;
    }

    // Try all possible indices for b[pos]
    for (int idx : indexMap.at(b[pos])) {
        if (!used[idx]) {
            current[pos] = idx;
            used[idx] = true;
            backtrack(b, indexMap, current, used, results, pos + 1);
            used[idx] = false;
        }
    }
}

// Function to find all valid permutation vectors
vector<vector<int>> findAllPermutationVectors(const vector<int> &a, const vector<int> &b) {
    int n = a.size();
    unordered_map<int, vector<int>> indexMap;

    // Build a mapping from value to indices in `a`
    for (int i = 0; i < n; ++i) {
        indexMap[a[i]].push_back(i);
    }

    // Prepare for backtracking
    vector<vector<int>> results;
    vector<int> current(n);
    vector<bool> used(n, false);

    // Start backtracking
    backtrack(b, indexMap, current, used, results, 0);

    return results;
}

int main() {
    string input1, input2;
    vector<int> a, b;

    // Read input sequences
    getline(cin, input1);
    getline(cin, input2);

    // Parse inputs into vectors
    parseInput(input1, a);
    parseInput(input2, b);

    // Find all valid permutation vectors
    vector<vector<int>> results = findAllPermutationVectors(a, b);

    // Output all valid permutation vectors
    for (const auto &perm : results) {
        for (size_t i = 0; i < perm.size(); ++i) {
            cout << perm[i];
            if (i < perm.size() - 1) {
                cout << ",";
            }
        }
        cout << endl;
    }

    return 0;
}
