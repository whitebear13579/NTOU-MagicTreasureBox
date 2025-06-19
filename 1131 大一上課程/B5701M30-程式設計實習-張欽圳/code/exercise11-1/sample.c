#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <ctype.h>

#define isEOS(x)  (x == -1 || x == '\0')

void add_word(char *word, char word_array[][61], int *n_diff_word) {
    for (int i = 0; i < *n_diff_word; i++) {
        if (strcmp(word_array[i], word) == 0) {
            return;
        }
    }
    strcpy(word_array[*n_diff_word], word);
    (*n_diff_word)++;
}//

int main() {
    int ch;
    int state = 0;
    int n_number = 0;
    int n_word = 0, n_diff_word = 0;
    int wid = 0;
    char aword[61];
    char word_array[100][61]; 

    while (state != 4) {
        ch = getchar();
        switch (state) {
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
                } else { // others
                    state = 3;
                }
                break;

            case 1:
                if (isalpha(ch)) {
                    aword[wid] = ch;
                    wid++;
                } else {
                    aword[wid] = '\0';
                    add_word(aword, word_array, &n_diff_word);
                    n_word++;
                    wid = 0;
                    if (isdigit(ch)) {
                        state = 2;
                        n_number++;
                    } else if (isEOS(ch)) {
                        state = 4;
                    } else {
                        state = 3;
                    }
                }
                break;

            case 2:
                if (!isdigit(ch)) {
                    if (isalpha(ch)) {
                        state = 1;
                        aword[wid] = ch;
                        wid = 1;
                    } else if (isEOS(ch)) {
                        state = 4;
                    } else {
                        state = 3;
                    }
                }
                break;

            case 3:
                if (isalpha(ch)) {
                    state = 1;
                    aword[wid] = ch;
                    wid = 1;
                } else if (isdigit(ch)) {
                    state = 2;
                    n_number++;
                } else if (isEOS(ch)) {
                    state = 4;
                }
                break;

            case 4:
                break;
        }
    }
    printf("%d\n", n_word);
    printf("%d\n", n_number);
    printf("%d\n", n_diff_word);

    return 0;
}