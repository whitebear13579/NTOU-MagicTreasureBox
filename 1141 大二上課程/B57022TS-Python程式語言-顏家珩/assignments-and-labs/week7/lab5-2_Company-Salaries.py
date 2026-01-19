'''

lab 5-2 Company Salaries
company_salaries.csv為某公司 100 位員工的基本資料，
欄位包括：職位、年資、薪水、學歷、性別、年齡
請讀取 company_salaries.csv (with open指令可加上encoding=“utf-8”，避免文字亂碼)
請新增以下三筆新員工資料，並將更新後的內容覆蓋回原始 csv檔，最後統計並印出公司總人數、最多人的學歷、最多人的性別。
輸出說明
更改後的csv檔案末尾，會有新增的資料
統計的結果:
公司總人數：103人
最多人的學歷：碩士
最多人的性別：男
'''

import csv

# Step 1：讀取原始資料
in_file_path = "./company_salaries.csv"
original_data = []

# 欄位名稱
fieldnames = [] 

with open(in_file_path, mode='r', encoding='utf-8', newline='') as file:
    reader = csv.DictReader(file)
    fieldnames = reader.fieldnames
    original_data = list(reader)


# Step 2：建立要新增的三筆資料
new_data = [
    {"職位": "資訊處處長", "年資": 8, "薪水": 70560, "學歷": "博士", "性別": "男", "年齡": 37},
    {"職位": "保全", "年資": 4, "薪水": 34000, "學歷": "碩士", "性別": "男", "年齡": 53},
    {"職位": "工程師", "年資": 13, "薪水": 153000, "學歷": "碩士", "性別": "男", "年齡": 38}
]

# Step 3：合併新資料
merge = original_data + new_data # 使用 list 的合併

# 將合併後的資料寫回
with open('company_salaries.csv', mode='w', encoding='utf-8', newline='') as file:
    writer = csv.DictWriter(file, fieldnames=fieldnames)
    writer.writeheader()
    writer.writerows(merge)


print(f"公司總人數：{len(merge)}人")
# 統計學歷
education_counts = {}
for row in merge:
    education = row['學歷']
    education_counts[education] = education_counts.get(education, 0) + 1

most_common_education = max(education_counts, key=education_counts.get)
print(f"最多人的學歷：{most_common_education}")

# 統計性別
gender_counts = {}
for row in merge:
    gender = row['性別']
    gender_counts[gender] = gender_counts.get(gender, 0) + 1
    
most_common_gender = max(gender_counts, key=gender_counts.get)
print(f"最多人的性別：{most_common_gender}")


'''
使用 pandas library 完成作業：

import pandas as pd
#hint pd.read_csv 讀取資料
#詳細請自行查詢 pandas 

# Step 1：讀取原始資料
in_csv = pd.read_csv("./company_salaries.csv" ,encoding='utf-8')

# Step 2：建立要新增的三筆資料
new_data = [
    {"職位": "資訊處處長", "年資": 8, "薪水": 70560, "學歷": "博士", "性別": "男", "年齡": 37},
    {"職位": "保全", "年資": 4, "薪水": 34000, "學歷": "碩士", "性別": "男", "年齡": 53},
    {"職位": "工程師", "年資": 13, "薪水": 153000, "學歷": "碩士", "性別": "男", "年齡": 38}
]
add_data = pd.DataFrame(new_data)
merge = pd.concat([in_csv,add_data], ignore_index=True)


# Step 3：合併新資料
#merge.to_csv('output.csv',index=False)
merge.to_csv('company_salaries.csv',index=False)

# Step 4：統計結果
print(f"公司總人數：{len(merge)}人")
print(f"最多人的學歷：{merge['學歷'].mode()[0]}")
print(f"最多人的性別：{merge['性別'].mode()[0]}")
'''