def solution(in_string, tar) -> float:
    total_count = 0
    letters = in_string.split()
    total_count += (len(letters)-1)*0.3 # spacing
    for i in letters:
        if i.find(tar) != -1:
            now = i
            while now.find(tar) != -1:
                total_count += 0.4
                now = now.replace(tar, '', 1)
            total_count += len(now)*0.3

        else:
            total_count += len(i)*0.3
    
    return total_count

uwu = input()
target = input()
print(f"{solution(uwu, target):.1f}")