#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include <cstring>

struct studentRecord{
    char name[10];
    char id[10];
    int  age;
    char email[64];
};

int cmp(const void* p1, const void* p2){
    const struct studentRecord *p = (const struct studentRecord*)p1;
    const struct studentRecord *q = (const struct studentRecord*)p2;
    return strcmp(p->id,q->id);
}

void inputStudentRecord(struct studentRecord* data)
{
     scanf("%s%s%d%s",data->name,data->id,&data->age,data->email);
}
void outputStudentRecord(const struct studentRecord*);
void sortStudentRecord(struct studentRecord [],int n);

#define MAXSIZE 100
int main(void)
{
   int n,i;
    struct studentRecord students[MAXSIZE];
    scanf("%d",&n);
   for(i = 0; i < n; ++i) {
      inputStudentRecord(students + i); //&students[i]
    }
    sortStudentRecord(students, n);
    for(i = 0; i < n; ++i) {
       outputStudentRecord(students + i); 
    }
   return 0;
}

void sortStudentRecord(struct studentRecord data[],int n){
    qsort(data,n,sizeof(struct studentRecord),cmp);
}

void outputStudentRecord(const struct studentRecord* data){
    printf("%s %s %d %s\n",data->name,data->id,data->age,data->email);
}
 