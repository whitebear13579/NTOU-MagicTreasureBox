#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<ctype.h>
#include<stdbool.h>

#define isEOS(x)  (x==-1||x=='\0')

// 函數：將單詞添加到單詞陣列中
void add_word(char*word,char word_array[][61],int n_word){
    strcpy(word_array[n_word],word);
}

int main(){
    int ch;
    int state = 0;  // 狀態變數
    int n_number = 0;  // 整數數字總數
    int n_word  = 0, n_diff_word=0;  // 單詞總數與不同單詞總數
    int wid = 0,i,j;
    char aword[61];  // 單詞緩衝區
    char word_array[500][61];  // 單詞陣列

    while(state != 4) {  // 狀態機循環，直到結束狀態
        ch = getchar();  // 讀取一個字元
        switch(state) {
            case 0:
                if (isalpha(ch)) {
                    state = 1;
                    aword[wid] = ch;
                    wid = 1;
                } else if (isdigit(ch)) {
                    state = 2;
                    n_number++;
                } else if (isEOS(ch)) {
                    state = 4;
                } else {
                    state = 3;
                }
                break;

            case 1: //word
                if (isalpha(ch)) {
                    // 持續累計字母
                    aword[wid] = ch;
                    wid++;
                } else{
                    aword[wid] = '\0';
                    add_word(aword,word_array,n_word);
                    n_word++;
                    wid = 0;
                    if (isdigit(ch)) {
                        n_number++;
                        state = 2;
                    } else if (isEOS(ch)) {
                        state = 4;
                    } else {
                        state = 3;
                    }
                }
                break;

            case 2: //number
                if (!isdigit(ch)){
                    if (isalpha(ch)){
                        state = 1;
                        aword[wid] = ch;
                        wid++;
                    }else if (isEOS(ch)){
                        state = 4;
                    }else{
                        state = 3;
                    }
                }
                break;
            
            case 3:
                if(isalpha(ch)){
                    state = 1;
                    aword[wid] = ch;
                    wid = 1;
                }else if (isdigit(ch)){
                    state = 2;
                    n_number++;
                }else if (isEOS(ch)){
                    state = 4;
                }else{
                    state = 3;
                }
                break;

            case 4:
                break;
        }
    }

    for(i = 0, n_diff_word = 0; i < n_word; ++i) {
        bool isUnique = 1;
        for(j = 0; j < i; ++j) {
            if(i != j && strcmp(word_array[i], word_array[j]) == 0){
                isUnique = 0;
                break;
            }
        }
        if(isUnique) n_diff_word++;
    }

    printf("%d\n",n_word);       
    printf("%d\n",n_number);     
    printf("%d\n",n_diff_word); 

    return 0;
}