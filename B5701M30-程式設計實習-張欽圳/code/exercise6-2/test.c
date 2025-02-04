#define _CRT_SECURE_NO_WARNING
#include <stdio.h>

/**
 * @brief 主函數，用於計算給定數字的最便宜進制。
 * 
 * 該程式讀取一系列成本和數字，然後根據給定的成本計算每個數字的最便宜進制。
 * 
 * @return int 成功執行後返回0。
 */
int main() {
    int cost[40] = { 0 };
    int x, num, e = 0, min = 100;
    long long int y = 0, ans = 0;

    // 讀取測試案例的數量
    scanf("%d", &x);

    for (int i = 0; i < x; i++) {
        // 讀取每個進制從2到36的成本
        for (int j = 0; j < 36; j++) {
            scanf("%d", &cost[j]);
        }

        // 讀取要處理的數字數量
        scanf("%d", &num);

        for (int k = 0; k < num; k++) {
            ans = 0;
            int total[40] = { 0 };
            // 處理每個數字的進制從2到36
            for (int m = 2; m <= 36; m++) {
                scanf("%lld", &e);

                y = e;
                while (y > 0) {
                    ans = y%m;
                    total[m] += cost[ans];
                    y/=m;
                }
                if ( min == 100 || total[m] < min ) min = total[m];
            }

            // 打印當前數字的最便宜進制
            printf("Cheapest base(s) for number %lld:", e);
            for (int q = 2; q < 36; q++) {
                if ( total[q] == min ){
                    printf(" %lld",q);
                }
            }
        }
    }
}