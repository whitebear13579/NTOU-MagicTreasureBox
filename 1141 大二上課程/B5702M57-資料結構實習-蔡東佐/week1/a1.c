#include <stdarg.h>
#include <stdio.h>
#include <climits>

void printInteger ( int n ){
    if ( n < 0 ){
        putc('-', stdout);
        n = -n;
    }
    if ( n > 9 ){
        printInteger(n/10);
    }
    putc('0' + (n%10), stdout);
}

void myprintf(const char *format, ...){
    va_list args;
    for ( va_start(args, format) ; *format != '\0' ; format++ ){
        if (*format == '%' && *(format + 1) != '\0') {
            format++;
            switch (*format){
                case 'd' : {
                    int i = va_arg(args, int);
                    printInteger(i);
                    break;
                }
                case 'c' : {
                    int c = va_arg(args, int);
                    putchar(c);
                    break;
                }
                case 's' : {
                    char* s = va_arg(args, char*);
                    fputs(s, stdout);
                    break;
                }
                default : {
                    putchar('%');
                    putchar(*format);
                    break;
                }
            }
        } else {
            putchar(*format);
        }
    }  
    va_end(args);
}


int main(){
    myprintf("string test: %s\n", "123456");
    myprintf("int max: %d\n", INT_MAX);
    myprintf("int negative: %d\n", -1243);
    myprintf("char %c %c %c \n", 'a', 'b', 'c');
    myprintf("mixed: %c %s %d \n", 'a', "abc", 1234);
    return 0;
}