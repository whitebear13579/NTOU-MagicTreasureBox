class Solution:
    def merge_sort(self, arr):
        if len(arr) <= 1:
            return arr

        mid = len(arr) // 2
        left_half = arr[:mid]
        right_half = arr[mid:]

        sorted_left = self.merge_sort(left_half)
        sorted_right = self.merge_sort(right_half)

        merged_list = []
        i = 0 
        j = 0

        while i < len(sorted_left) and j < len(sorted_right):
            if sorted_left[i] < sorted_right[j]:
                merged_list.append(sorted_left[i])
                i += 1
            else:
                merged_list.append(sorted_right[j])
                j += 1

        while i < len(sorted_left):
            merged_list.append(sorted_left[i])
            i += 1
            
        while j < len(sorted_right):
            merged_list.append(sorted_right[j])
            j += 1
        
        return merged_list

arr = list(map(int, input().split(',')))
sol = Solution()
print(*sol.merge_sort(arr), sep=',')