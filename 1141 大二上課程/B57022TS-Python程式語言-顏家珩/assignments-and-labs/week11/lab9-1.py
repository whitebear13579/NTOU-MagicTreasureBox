import pandas as pd

data = pd.read_csv('./aqx_p_488.csv', encoding='utf-8')

while True:
    try:
        oper = int(input("請輸入功能(1-4):"))
        if oper == 1:
            # 算全 CSV 平均 AQI, 最高 AQI, 最低 AQI, AQI 標準差
            avg_aqi = round(data['aqi'].mean())
            max_aqi = round(data['aqi'].max())
            min_aqi = round(data['aqi'].min())
            std_aqi = round(data['aqi'].std())
            print(f"平均 AQI: {avg_aqi:.1f}\n最高 AQI: {max_aqi:.1f}\n最低 AQI: {min_aqi:.1f}\nAQI 標準差: {std_aqi:.1f}")
        elif oper == 2:
            # 算全 CSV 平均 PM2.5, 最高 PM2.5, 最低 PM2.5, PM2.5 標準差
            avg_pm25 = round(data['pm2.5_conc'].mean())
            max_pm25 = round(data['pm2.5_conc'].max())
            min_pm25 = round(data['pm2.5_conc'].min())
            std_pm25 = round(data['pm2.5_conc'].std())
            print(f"平均 PM2.5: {avg_pm25:.1f}\n最高 PM2.5: {max_pm25:.1f}\n最低 PM2.5: {min_pm25:.1f}\nPM2.5 標準差: {std_pm25:.1f}")
        elif oper == 3:
            # 輸出每個縣市的平均 AQI（由大到小排序）<縣市>: <平均AQI>
            city_avg_aqi = data.groupby('county')['aqi'].mean().sort_values(ascending=False)
            for city, avg_aqi in city_avg_aqi.items():
                print(f"{city}: {avg_aqi:.1f}")
        elif oper == 4:
            # 找出每個縣市 AQI 最高的一筆資料 <縣市> <測站> <AQI> <時間>
            city_max_aqi = data.loc[data.groupby('county')['aqi'].idxmax()]
            for _, row in city_max_aqi.iterrows():
                print(f"{row['county']} {row['sitename']} {round(row['aqi']):.1f} {row['datacreationdate']}")
        else:
            print("Error")
            continue
    except ValueError:
        print("Error")
        continue