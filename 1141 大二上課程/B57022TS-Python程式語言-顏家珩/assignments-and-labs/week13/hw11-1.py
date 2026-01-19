import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib.ticker as mticker

plt.rcParams['font.sans-serif'] = ['Microsoft JhengHei'] 
plt.rcParams['axes.unicode_minus'] = False
sns.set_theme(style="whitegrid", font='Microsoft JhengHei')

def load_for_pyramid():
    df = pd.read_excel('縣市人口按性別及五齡組.xlsx', header=2, thousands=',')
    df.columns = df.columns.astype(str)
    df = df.loc[:, ~df.columns.str.contains('Unnamed')]
    df = df.drop(index=0).reset_index(drop=True)
    col_region = df.columns[0]
    df[col_region] = df[col_region].ffill()
    for col in df.columns[3:]:
        df[col] = pd.to_numeric(df[col], errors='coerce').fillna(0)
    original_5_to_100 = list(df.columns[5:])
    df['0～4'] = df.iloc[:, 3] + df.iloc[:, 4]
    ordered_age_cols = ['0～4'] + original_5_to_100
    return df, ordered_age_cols

def load_trend_data():
    all_data = []
    years = range(103, 114) 
    for year in years:
        try:
            df = pd.read_excel(
                '縣市人口按性別及五齡組.xlsx', 
                sheet_name=str(year), 
                header=2, 
                thousands=','
            )
            df.columns = df.columns.astype(str)
            df = df.loc[:, ~df.columns.str.contains('Unnamed')]
            df = df.drop(index=0).reset_index(drop=True)
            col_region = df.columns[0]
            df[col_region] = df[col_region].ffill()
            for col in df.columns[3:]:
                df[col] = pd.to_numeric(df[col], errors='coerce').fillna(0)
            
            young = df.iloc[:, 3] + df.iloc[:, 4] + df.iloc[:, 5] + df.iloc[:, 6]
            old_keywords = ['65', '70', '75', '80', '85', '90', '95', '100']
            old_cols = [c for c in df.columns if any(k in c for k in old_keywords)]
            old = df[old_cols].sum(axis=1)
            aging_idx = (old / young) * 100
            
            df['年份'] = year
            df['老化指數'] = aging_idx
            
            mask_county = (
                (df['性別'] == '計') & 
                (~df[col_region].str.contains('總|省', na=False))
            )
            df_county = df.loc[mask_county, ['年份', col_region, '老化指數']].copy()
            df_county.columns = ['年份', '縣市', '老化指數']
            
            mask_total = (
                (df['性別'] == '計') & 
                (df[col_region].str.contains('總', na=False))
            )
            df_total = df.loc[mask_total, ['年份', col_region, '老化指數']].copy()
            df_total['縣市'] = '總計'
            df_total = df_total[['年份', '縣市', '老化指數']]
            
            final = pd.concat([df_county, df_total])
            
            final = final.dropna(subset=['縣市'])
            all_data.append(final)
            
        except Exception as e:
            pass
            
    if all_data:
        return pd.concat(all_data, ignore_index=True)
    return pd.DataFrame()

def draw_pyramid(df_raw, age_groups, ax):
    mask_taiwan = df_raw.iloc[:, 0].str.contains('總', na=False)
    df_tw = df_raw[mask_taiwan].copy()
    cols_to_use = ['性別'] + age_groups
    df_tw = df_tw[cols_to_use]
    df_melted = df_tw.melt(id_vars=['性別'], 
                            var_name='Age', 
                            value_name='Population')
    df_melted = df_melted[df_melted['性別'] != '計']
    df_melted['性別'] = df_melted['性別'].map({'男': '男性', '女': '女性'})
    df_melted.loc[df_melted['性別'] == '男性', 'Population'] *= -1
    age_order = age_groups[::-1]

    sns.barplot(
        data=df_melted, 
        x='Population', y='Age', hue='性別', 
        order=age_order,
        palette={'男性': '#6699CC', '女性': '#FF9999'},
        dodge=False,
        ax=ax
    )

    ax.set_title('台灣人口金字塔 (113年)', fontsize=16, fontweight='bold')
    ax.set_xlabel('人口數', fontweight='bold')
    ax.set_ylabel('年齡組（歲）', fontweight='bold')
    ax.xaxis.set_major_formatter(
        mticker.FuncFormatter(lambda x, pos: f'{int(abs(x)):,}')
    )
    ax.legend(loc='lower right')
    ax.grid(axis='x', alpha=0.5)

def draw_trend(df_trend, ax):
    if df_trend.empty:
        return

    df_trend = df_trend.dropna(subset=['縣市'])

    hue_order = df_trend['縣市'].unique().tolist()
    hue_order = [x for x in hue_order if str(x) != 'nan']
    
    if '總計' in hue_order:
        hue_order.remove('總計')
        hue_order.insert(0, '總計')

    sns.lineplot(
        data=df_trend,
        x='年份', y='老化指數', hue='縣市',
        hue_order=hue_order,
        style='縣市',
        markers=True,
        dashes=False,
        palette='tab20', linewidth=1.8, alpha=0.8,
        ax=ax
    )

    ax.set_title(
        '各縣市老化指數趨勢（103-113年）', 
        fontsize=16, 
        fontweight='bold'
    )
    ax.set_ylabel('老化指數', fontweight='bold')
    ax.set_xlabel('年份',fontweight='bold')
    yr_range = range(103, 114)
    ax.set_xticks(yr_range)
    ax.set_xticklabels([f'{y}年' for y in yr_range])
    ax.legend(
        loc='upper left',
        ncol=3,
        fontsize=9,
        framealpha=0.9
    )
    ax.grid(True, alpha=0.3)
    
    ax.text(0.98, 0.02, '老化指數 = (65歲以上 / 0-14歲) × 100%', 
            transform=ax.transAxes, 
            ha='right', va='bottom', fontsize=10, color='black',
            bbox=dict(boxstyle='round,pad=0.5',
                    facecolor='#FFFFE0',
                    edgecolor='black',
                    linewidth=1)
            )

if __name__ == "__main__":
    df_pyr, age_list = load_for_pyramid()
    df_trend = load_trend_data()
    
    fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(18, 8))

    draw_pyramid(df_pyr, age_list, ax1)    
    draw_trend(df_trend, ax2)

    plt.suptitle(
        '台灣人口結構與老化趨勢分析',
        fontweight='bold',
        fontsize=20
    )
    plt.subplots_adjust(top=0.88, wspace=0.15)
    
    plt.show()