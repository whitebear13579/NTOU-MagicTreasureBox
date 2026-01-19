import pandas as pd
from sklearn.datasets import load_iris
from sklearn.preprocessing import StandardScaler
from sklearn.cluster import KMeans

iris = load_iris()

# data preprocessing
df = pd.DataFrame(
    data=iris.data,
    columns=iris.feature_names
)
df['target'] = iris.target
df['species'] = df['target'].map(
    {0: 'setosa', 1: 'versicolor', 2: 'virginica'}
)
scaler = StandardScaler()
scaled_data = scaler.fit_transform(df[iris.feature_names])

# using kmeans algo to group data ( 3 group )
kmeans = KMeans(
    n_clusters=3,
    random_state=114514,
    n_init=10
)

df['cluster'] = kmeans.fit_predict(scaled_data)
group = df.groupby('cluster')

# output
for i, data in group:
    most_common = data['species'].mode()[0]
    print(f"Cluster {i}: {most_common}")

