#include <iostream>
#include <vector>
using namespace std;

class Student {
public:
    // Check Point 1 (5%)
    Student(string name, int student_id, int math_score, int english_score) {
        this->name = name;
        this->student_id = student_id;
        this->math_score = math_score;
        this->english_score = english_score;
        this->total_score = math_score+english_score;
    }
    string name;
    int student_id;
    int math_score;
    int english_score;
    int total_score;

    // Check Point 3 (5%)
    virtual void print() {
        cout << "Name: " << name << endl;
        cout << "Student ID: " << student_id << endl;
        cout << "Math Score: " << math_score << endl;
        cout << "English Score: " << english_score << endl;
        cout << "Total Score: " << total_score << endl;
    };
};

// Check Point 2 (55%) Please also refer to main().
void mySort(vector<Student> & students) {
    for (int i = 0; i < students.size(); i++) {
        int minIndex = i;
        for (int j = i+1; j < students.size(); j++) {
            if (students[j].student_id < students[minIndex].student_id) {
                minIndex = j;
            }
        }
        if (minIndex != i) {
            Student t = students[i];
            students[i] = students[minIndex];
            students[minIndex] = t;
        }
    }
}

// Check Point 4 (5%)
class HighSchoolStudent: public Student {
public:
    HighSchoolStudent(string name, int student_id, int math_score, int english_score):
        Student(name, student_id, math_score, english_score) {
    }
    string highschool_name;
    void print() {
        cout << "Name: " << name << endl;
        cout << "Student ID: " << student_id << endl;
        cout << "Category: " << "HighSchoolStudent" << endl;
        cout << "Math Score: " << math_score << endl;
        cout << "English Score: " << english_score << endl;
        cout << "Total Score: " << total_score << endl;
    }
};

// Check Point 4 (5%)
class CollegeStudent: public Student {
public:
    CollegeStudent(string name, int student_id, int math_score, int english_score):
        Student(name, student_id, math_score, english_score) {
    }
    string college_name;
    void print() {
        cout << "Name: " << name << endl;
        cout << "Student ID: " << student_id << endl;
        cout << "Category: " << "CollegeStudent" << endl;
        cout << "Math Score: " << math_score << endl;
        cout << "English Score: " << english_score << endl;
        cout << "Total Score: " << total_score << endl;
    }
};

class TotalScoreCmp {
public:
    bool operator()(shared_ptr<Student> s1, shared_ptr<Student> s2)
        {return s1->total_score < s2->total_score; }
};

class StudentManager {
public:
    vector<shared_ptr<Student>> students;
    // Check Point 5 (5%)
    void addStudent(shared_ptr<Student> s) {
        for (int i = 0; i < students.size(); i++) {
            if (students[i]->student_id == s->student_id) {
                return;
            }
        }
        students.push_back(s);
    }
    // Check Point 6 (5%)
    void printStudents() {
        for (int i = 0; i < students.size(); i++) {
            students[i]->print();
        }
    }
    
    // Check Point 7 (5%)
    void sortByTotalScore() {
        TotalScoreCmp cmp;
        sort(students.begin(), students.end(), cmp);
    }

    // Check Point 8 (5%)
    void sortByMathScoreThenEnglishScore() {
        sort(students.begin(), students.end(),
            [](shared_ptr<Student> s1, shared_ptr<Student> s2) {
                if (s1->math_score < s2->math_score) {
                    return true;
                } else if (s1->math_score == s2->math_score &&
                    s1->english_score < s2-> english_score) {
                        return true;
                }
                return false;
            });
    }

};

int main() {

    // Check Point 2 (55%) Demonstrate the correctness
    vector<Student> students = {
        Student("John", 5, 90, 80),
        Student("Bob", 3, 95, 85),
        Student("Jeff", 2, 85, 70)
    };
    mySort(students);
    for (int i = 0; i < students.size(); i++) {
        students[i].print();
    }

    StudentManager manager;
    HighSchoolStudent student0 = HighSchoolStudent("Wang", 6, 100, 100);
    HighSchoolStudent student1 = HighSchoolStudent("Wang", 8, 80, 80);
    CollegeStudent student2 = CollegeStudent("Lin", 7, 90, 90);
    CollegeStudent student3 = CollegeStudent("Kobayashi", 7, 90, 90);
    manager.addStudent(make_shared<Student>(student0));
    manager.addStudent(make_shared<Student>(student1));
    manager.addStudent(make_shared<Student>(student2));
    // Check Point 9 (5%) // Kobaysahi will not be added
    manager.addStudent(make_shared<Student>(student3));
    // Check Point 10 (5%)
    //manager.sortByTotalScore();
    manager.sortByMathScoreThenEnglishScore();
    manager.printStudents();
}