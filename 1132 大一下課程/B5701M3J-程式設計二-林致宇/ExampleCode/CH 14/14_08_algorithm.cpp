#include<iostream>
#include<algorithm>

int main() {
    int arr1[] = {30, 27, 19, 56, 72, 36};
    double arr2[] = {3.6, 7.2, 8.5, 1.6, 9.1, 5.6};

    std::cout << *std::min_element<int *>(&arr1[0], &arr1[5]) << std::endl;
    std::cout << *std::min_element<double *>(&arr2[0], &arr2[5]) << std::endl;
}












