#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

// Sleep sort works by creating a separate process for each number
// Each process sleeps for a duration proportional to its number
// When processes wake up, they print their number in order

int main(int argc, char *argv[]) {
    // Check if we have at least one number to sort
    if (argc < 2) {
        printf("Usage: %s <num1> <num2> ... <numN>\n", argv[0]);
        printf("Example: %s 5 3 6 3 1 8 7 2 4\n", argv[0]);
        return 1;
    }

    // For monitoring child processes
    int num_processes = argc - 1;
    pid_t pid;
    
    printf("Starting sleep sort for %d numbers...\n", num_processes);
    
    // Create one process for each number
    for (int i = 1; i < argc; i++) {
        // Convert argument to integer
        int num = atoi(argv[i]);
        
        // Validate input
        if (num <= 0) {
            printf("Error: All numbers must be positive integers\n");
            return 1;
        }
        
        // Fork a new process
        pid = fork();
        
        if (pid < 0) {
            // Error in forking
            perror("Fork failed");
            return 1;
        } 
        else if (pid == 0) {
            // Child process
            
            // Sleep for a time proportional to the number
            // Using a multiplier to make the sorting visible but not too slow
            // int sleep_time = num * 100000; // in microseconds (0.1 second per unit)
            int sleep_time = num * 100;
            usleep(sleep_time);
            
            // After sleeping, print the number
            printf("%d ", num);
            fflush(stdout); // Ensure output is displayed immediately
            
            // Exit the child process
            exit(0);
        }
        // Parent process continues to the next iteration
    }
    
    // Parent process waits for all child processes to complete
    for (int i = 0; i < num_processes; i++) {
        wait(NULL);
    }
    
    // Add a newline at the end of the sorted output
    printf("\nSleep sort completed!\n");
    
    return 0;
}