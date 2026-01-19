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
    # return all appear index(a list)
    return ans
