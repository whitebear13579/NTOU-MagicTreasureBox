'''

hw 6-4 Knuth-Morris-Pratt
請撰寫一個 Python 程式，使用 KMP（Knuth-Morris-Pratt）演算法在母字串 text 中搜尋子字串 pattern 的所有出現位置。
程式要求：
實作 KMP 演算法，包括 LPS（Longest Prefix Suffix）陣列計算
找出 pattern 在 text 中的所有匹配起始索引（0-based）
將匹配結果依序輸出
輸入說明：
輸入兩行：
第一行為母字串 text
第二行為要搜尋的字串 pattern
輸出說明：
輸出一行：所有匹配的起始索引，以空白分隔（若無匹配，輸出空行）
#input
abcdeabc
abc
#output
0 5

'''

def getLps( tar ):
    lps = [0] * len(tar)
    prefix = 0
    i = 1
    while i < len(tar):
        if tar[prefix] == tar[i]:
            prefix += 1
            lps[i] = prefix
            i += 1
        else:
            if prefix != 0:
                prefix = lps[prefix - 1]
            else:
                lps[i] = 0
                i += 1
    return lps

def kmp( orig_str, tar ):
    if not tar:
        return []
    lps = getLps(tar)
    ans = list()
    origPtr = 0
    targPtr = 0
    while origPtr < len(orig_str):
        if orig_str[origPtr] == tar[targPtr]:
            origPtr += 1
            targPtr += 1

        if targPtr == len(tar):
            ans.append(origPtr - targPtr)
            targPtr = lps[targPtr-1]
        elif origPtr < len(orig_str) and orig_str[origPtr] != tar[targPtr]:
            if targPtr != 0:
                targPtr = lps[targPtr-1]
            else:
                origPtr += 1  
    return ans

'''
    最浅显易懂的 KMP 算法讲解
    https://www.youtube.com/watch?v=af1oqpnH1vA
'''

string = str(input())
tar = str(input()) 
ans = kmp(string, tar)
if not ans:
    print()
else:
    print(" ".join(map(str, ans)))