#include <iostream>
#include <unistd.h>
#include <cstdlib>
#include <sys/wait.h>
using namespace std;

/*
    --------------------- Caution ---------------------
    THIS CODE CANNOT RUN AT WINDOWS SYSTEM.
    USE LINUX / UNIX BASED SYSTEM TO COMPILE THIS CODE
    OR USE THE WINDOWS WSL :D

    2025.09.26 NTOU CS2B 01357101
    ---------------------------------------------------
*/

inline bool isPrime( int n ){
    if (n < 2) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;
    
    for ( int i = 3 ; i * i <= n ; i += 2 ) {
        if ( n % i == 0 ){
            return false;
        }
    }
    return true;
}

signed main( int argc, char *argv[] ) {
    int st = 0, end = 0;
    cout << "Enter range: ";
    cin >> st >> end;
    
    int total = end - st + 1, interval = total / 4, remainder = total % 4;

    pid_t pid;
    int nowSt = st;

    for ( int i = 1 ; i <= 4 ; i++ ) {
        int nowInterval = interval;
        if ( i <= remainder ) { 
            nowInterval++;
        }
        int nowEnd = nowSt + nowInterval - 1;

        pid = fork();

        if ( pid < 0 ) {
            perror("Fork Failed!");
            return 1;
        } else if ( pid == 0 ) {
            int now = 0;
            cout << "Child process "<< i <<" handles range: " << nowSt << "-" << nowEnd << "\n";
            fflush(stdout);
            
            for ( int j = nowSt ; j <= nowEnd ; j++ ) {
                if ( isPrime(j) ) {
                    ++now;
                }
            }
            cout << "Child process "<< i <<" found "<< now <<" prime numbers\n";
            fflush(stdout);
            exit(now);
        }

        nowSt = nowEnd + 1;
    }

    int count = 0;
    for (int i = 0; i < 4; i++) {
        int status;
        wait(&status);
        if (WIFEXITED(status)) {
            count += WEXITSTATUS(status);
        }
    }
    
    cout << "Total prime numbers found: " << count << "\n";
    return 0;
}