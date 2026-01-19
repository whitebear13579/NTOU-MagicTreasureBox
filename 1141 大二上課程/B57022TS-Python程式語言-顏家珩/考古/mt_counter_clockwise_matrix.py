def Solution( num ):
    matrix = [[-1]*num for _ in range(num)]
    x, y = -1, -1
    if num%2 == 1:
        x, y = num//2, num//2
    else:
        x, y = num//2, num//2 - 1

    matrix[x][y] = 1

    dx = [0, -1, 0, 1]
    dy = [1, 0, -1, 0]
    dir = 0
    step, length = 2, 1

    while step <= num*num: 
        for i in range(2):
            for j in range(length):
                if step > num*num:
                    break
                x, y = x + dx[dir], y + dy[dir]

                if ( x >= 0 and x < num ) and ( y >= 0 and y < num ):
                    matrix[x][y] = step
                    step += 1

            dir = (dir + 1) % 4
        length += 1

    w = len(str(num*num))

    for row in matrix:
        print(" ".join(f"{i:>{w}}" for i in row))

n = int(input())
Solution(n)