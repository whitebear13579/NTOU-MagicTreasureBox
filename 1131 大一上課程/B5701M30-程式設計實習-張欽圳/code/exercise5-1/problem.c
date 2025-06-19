#include<stdio.h>
#include<string.h>
#include<ctype.h>

void morse(char gura);

int main()
{
    char b0,b1;
    int i = 0;
    while((b1=getchar())!=EOF) {
        if (isalnum(b1)){
            if(i>0&&isalnum(b0)){
                printf(" ");
            }
            morse(b1);
        } else if (b1 == ' ') {
            printf("       ");
        }
        b0=b1;
        i = 1;
    }
    printf("\n");
    return 0;
}

void morse(char gura){
    switch ( gura ){
    case 'A':
        printf(".-");
        break;
    case 'B':
        printf("-...");
        break;
    case 'C':
        printf("-.-.");
        break;
    case 'D':
        printf("-..");
        break;
    case 'E':
        printf(".");
        break;
    case 'F':
        printf("..-.");
        break;
    case 'G':
        printf("--.");
        break;
    case 'H':
        printf("....");
        break;
    case 'I':
        printf("..");
        break;
    case 'J':
        printf(".---");
        break;
    case 'K':
        printf("-.-");
        break;
    case 'L':
        printf(".-..");
        break;
    case 'M':
        printf("--");
        break;
    case 'N':
        printf("-.");
        break;
    case 'O':
        printf("---");
        break;
    case 'P':
        printf(".--.");
        break;
    case 'Q':
        printf("--.-");
        break;
    case 'R':
        printf(".-.");
        break;
    case 'S':
        printf("...");
        break;
    case 'T':
        printf("-");
        break;
    case 'U':
        printf("..-");
        break;
    case 'V':
        printf("...-");
        break;
    case 'W':
        printf(".--");
        break;
    case 'X':
        printf("-..-");
        break;
    case 'Y':
        printf("-.--");
        break;
    case 'Z':
        printf("--..");
        break;
    case '1':
        printf(".----");
        break;
    case '2':
        printf("..---");
        break;
    case '3':
        printf("...--");
        break;
    case '4':
        printf("....-");
        break;
    case '5':
        printf(".....");
        break;
    case '6':
        printf("-....");
        break;
    case '7':
        printf("--...");
        break;
    case '8':
        printf("---..");
        break;
    case '9':
        printf("----.");
        break;
    case '0':
        printf("-----");
        break;
    default:
        break;
    }
}