maze = list()
def Solution(n) -> int:
    dx = [0, -1, 0, 1]
    dy = [1, 0, -1, 0]
    end_x, end_y = n-1, n-1
    visted = [[False]*n for i in range(n)]
    
    def dfs(x, y):
        if x == end_x and y == end_y:
            return 1

        visted[x][y] = True
        ans = 0
        for i in range(4):
            nx, ny = x + dx[i], y + dy[i]
            if ( nx >= 0 and nx < n ) and ( ny >= 0 and ny < n ) and maze[nx][ny] == 0 and not visted[nx][ny]:
                ans += dfs(nx, ny)

        visted[x][y] = False
        return ans

    return dfs(0, 0)

n = int(input())
for i in range(n):
    row  = list(map(int, input().split()))
    maze.append(row)
print(Solution(n))