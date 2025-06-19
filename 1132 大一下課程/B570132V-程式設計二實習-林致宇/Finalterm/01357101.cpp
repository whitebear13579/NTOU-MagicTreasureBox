-#include <bits/stdc++.h>
#define whitebear ios_base::sync_with_stdio(0) , cin.tie(0) , cout.tie(0)
#define vi vector<int>
#define vii vector<vector<int>>
using namespace std;

//score: 100 / 100

class Student{
    public:
        string name;
        int student_id;
        int math_score;
        int english_score;
        int total_score;
        Student() = default;
        Student( string nname, int id, int math, int english ){ //task 1 
            name = nname, student_id = id, math_score = math, english_score = english;
            total_score = math + english;
        }

        virtual void print(){ //task 3
            cout << "Name: " << name << ", ID: " << student_id << ", Total Score: " << total_score << "\n";
        }
};

bool compare( shared_ptr<Student> a, shared_ptr<Student> b ){ //task 5
    return a->total_score > b->total_score;
}

void task2( vector<Student> &data, int n ){ // task 2 學號由小到大排序
    for ( int i = 0 ; i < n ; i++ ){
        for ( int j = i+1 ; j <= n-1 ; j++ ){
            if ( data[i].student_id > data[j].student_id  ){
                auto tp = data[i];
                data[i] = data[j];
                data[j] = tp;
            }
        }
    }
}

//task 4
class HighSchoolStudent : public Student{
    public:
        string highschool_name;
        HighSchoolStudent( string nname, int id, int math, int english ){
            name = nname, student_id = id, math_score = math, english_score = english;
            total_score = math + english;
            highschool_name = nname;
        };
        void print() override{
            cout << "Name: " << highschool_name << ", Category: HighSchoolStudent, Student ID: " << student_id << ", Math Score: " << math_score << ", English Score: " << english_score << ", Total: " << total_score << "\n";
        }
};

class CollegeStudent : public Student{
    public:
        string college_name;
        CollegeStudent( string nname, int id, int math, int english ){
            name = nname, student_id = id, math_score = math, english_score = english;
            total_score = math + english;
            college_name = nname;
        };
        void print() override{
            cout << "Name: " << college_name << ", Category: CollegeStudent, Student ID: " << student_id << ", Math Score: " << math_score << ", English Score: " << english_score << ", Total: " << total_score << "\n";
        }
};

//task 5
class StudentManager{
    public:
        vector<shared_ptr<Student>> students;
        void addStudent ( shared_ptr<Student> s){
            bool duplicate = 0;
            for ( auto i : students ){ // check the students id is already exist.
                if ( i->student_id == s->student_id ) duplicate = 1;
            }
            if ( !duplicate ) students.push_back(s);
        }

        void printStudents(){
            for ( auto i : students ) i->print();
        }

        void sortByTotalScore(){ //Use Function Object
            sort(students.begin(), students.end(), compare);
        }

        void sortByMathScoreThenEnglishScore(){ //Use lambda Expression
            sort(students.begin(), students.end(), [](shared_ptr<Student> a, shared_ptr<Student> b){
                if ( a->math_score == b->math_score ) return a->english_score > b->english_score;
                else return a->math_score > b->math_score;
            });
        }
};


inline signed solve(){
    vector<Student> data;
    int n = 10;
    // task 1 & task 2
    data.push_back({"John",8,60,80});
    data.push_back({"Peter",4,30,30});
    data.push_back({"Mandy",1,90,90});
    data.push_back({"Harry",2,40,60});
    data.push_back({"Miya",10,100,100});
    data.push_back({"Ricky",3,70,70});
    data.push_back({"Monky",6,90,40});
    data.push_back({"Lucy",5,70,70});
    data.push_back({"Vicky",9,20,10});
    data.push_back({"Hank",7,80,90});
    task2(data,n);
    //verify task 2
    cout << "> Task2:\n";
    for ( auto i : data ){
        cout << i.name << " " << i.student_id << "\n";
    }
    //verify task 3
    cout << "> Task3:\n";
    data[0].print();
    //verify task 4
    CollegeStudent t1("John", 11011, 90, 80);
    HighSchoolStudent t2("Bob", 11011, 95, 70);
    cout << "> Task4:\n";
    t1.print();
    t2.print();
    //verify task 6
    StudentManager db;
    HighSchoolStudent t3("Mohny", 11012, 60, 40);
    CollegeStudent t4("Peter", 11013, 70, 20);
    HighSchoolStudent t5("Harry", 11014, 100, 90);
    HighSchoolStudent t6("Vicky", 11015, 60, 20);
    CollegeStudent t7("Miya", 11016, 70, 30);
    db.addStudent(make_shared<CollegeStudent>(t1)), db.addStudent(make_shared<HighSchoolStudent>(t2)), db.addStudent(make_shared<HighSchoolStudent>(t3)), db.addStudent(make_shared<CollegeStudent>(t4)), db.addStudent(make_shared<HighSchoolStudent>(t5)), db.addStudent(make_shared<HighSchoolStudent>(t6)), db.addStudent(make_shared<CollegeStudent>(t7));
    cout << "> Task6: | Correctness of addStudent()\n";
    db.printStudents();
    cout << "> Task6: | Correctness of sortByTotalScore()\n";
    db.sortByTotalScore();
    db.printStudents();
    cout << "> Task6: | Correctness of sortByMathScoreThenEnglishScore()\n";
    db.sortByMathScoreThenEnglishScore();
    db.printStudents();
    // uwu owo ouo 030 xwx 
    return 0;
}

int main(){
    whitebear;
    solve();
    return 0;
}