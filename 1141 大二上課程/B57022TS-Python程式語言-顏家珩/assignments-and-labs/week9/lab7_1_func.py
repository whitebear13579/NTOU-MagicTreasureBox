import re
# lab7_1_func.py
def word_analyzer( string ) -> dict:
    string = re.sub(r"[^\w\s]","",string)
    string.lower()
    word_list = string.split()

    res = dict()
    for i in word_list:
        if i in res:
            res[i] += 1
        else:
            res[i] = 1
    res = dict(sorted(res.items(), key=lambda item: item[1], reverse=True))
    return res

def letter_analyzer( string ) -> dict:
    string = string.lower()
    string = re.sub("[^a-z]","",string)
    res = dict()
    for i in string:
        if i in res:
            res[i] += 1
        else:
            res[i] = 1
    res = dict(sorted(res.items(), key=lambda item: item[1], reverse=True))
    return res
    