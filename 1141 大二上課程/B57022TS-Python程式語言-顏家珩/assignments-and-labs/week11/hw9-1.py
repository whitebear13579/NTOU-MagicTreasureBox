'''
hw9-1.py
generate text1-text4 string
'''
import pandas as pd
import re


# 111年4月4日至111年4月10日資料.ods 前是舊格式
# 111年4月11日至111年4月17日資料.ods 開始是新格式

file_df = pd.read_csv("hw-data/file.csv")
all_data = []

for filename in file_df['name']:
    file_path = f"hw-data/{filename}"
    try:
        raw_df = pd.read_excel(file_path, engine='odf', header=None)
        match = re.search(r'(\d+)年(\d+)月(\d+)日', filename)
        if not match:
            continue
        year = int(match.group(1))
        month = int(match.group(2))
        day = int(match.group(3))
        
        if year < 112 or year > 113:
            continue
        
        if len(raw_df) < 9:
            continue
        
        categories = raw_df.iloc[4].astype(str).str.strip().tolist()
        
        occurrences_series = pd.to_numeric(raw_df.iloc[5], errors='coerce')
        solved_series = pd.to_numeric(raw_df.iloc[8], errors='coerce')
        
        for i in range(2, len(categories)):
            category = categories[i]
            
            if not category or category in ['nan', '即時犯罪資料(週報專用,104年3月啟用)', '案類別', '發生合計', '破獲合計']:
                continue
            
            occ_val = occurrences_series.iloc[i] if i < len(occurrences_series) else pd.NA
            sol_val = solved_series.iloc[i] if i < len(solved_series) else pd.NA
            
            occurrence = int(occ_val) if pd.notna(occ_val) else None
            solved_count = int(sol_val) if pd.notna(sol_val) else None
            
            all_data.append({
                '年份': year,
                '案件類別': category,
                '發生數': occurrence,
                '破獲數': solved_count
            })
    except Exception as e:
        print(f"Err: {filename}: {e}")

merged_df = pd.DataFrame(all_data)

while True:
    try:
        oper = int(input("請輸入功能(1~4):"))
        if oper == 1:
            '''
            output:
            - 合併後發生量總和 (112-113年) (int)
            - 發生量總和前三多的 "案件類別" <類別>:<發生量總和> (int)
            '''
            data_112_113 = merged_df[merged_df['年份'].between(112, 113)]
            total_raw = data_112_113['發生數'].sum()
            total_occurrence = int(total_raw) if not pd.isna(total_raw) else 0
            numeric_cols = ['發生數', '破獲數', '破獲率']
            print(f"合併後發生量總和:{total_occurrence}")
            top_categories = data_112_113.groupby('案件類別')['發生數'].sum().sort_values(ascending=False).head(3)
            print("前三類別:")
            for category, count in top_categories.items():
                print(f"{category}:{int(count)}")
        elif oper == 2:
            '''
            output:
            - 113 年發生量總和最多的前三個 "案件類別" <類別>:<發生量總和> (int)
            '''
            year_data = merged_df[merged_df['年份'] == 113]
            top_categories = year_data.groupby('案件類別')['發生數'].sum().sort_values(ascending=False).head(3)
            for category, count in top_categories.items():
                print(f"{category}:{int(count)}")
        elif oper == 3:
            '''
            output:
            - 112-113 年間各案件類別平均發生量前三名 (float, precision 2)
            '''
            print("平均發生量前三:")
            data_112_113 = merged_df[merged_df['年份'].between(112, 113)]
            avg_occurrence = data_112_113.groupby('案件類別')['發生數'].mean()
            top_avg = avg_occurrence.sort_values(ascending=False).head(3)
            for category, avg in top_avg.items():
                print(f"{category}:{avg:.2f}")
        elif oper == 4:
            '''
            output:
            - 113年案件總數 (int)
            - 113年破獲率平均值 (float, precision 2)
            - 從 113 年 1 月 1 日該筆資料計算到"113年12月30日至114年1月5日資料.ods"
            '''
            year_data = merged_df[merged_df['年份'] == 113]
            total_occurrence = year_data['發生數'].sum()
            total_solved = year_data['破獲數'].sum()
            case_total = int(total_occurrence) if not pd.isna(total_occurrence) else 0
            avg_solve_rate = total_solved / total_occurrence if total_occurrence > 0 else 0
            print(f"113案件總數:{case_total}")
            print(f"平均破獲率:{avg_solve_rate:.2f}")
        else:
            print("Error:Invalid input")
            continue
    except ValueError:
        print("Error:Invalid input")
        continue
    except EOFError:
        break
