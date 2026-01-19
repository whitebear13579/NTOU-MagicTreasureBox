import pandas as pd
from sklearn.datasets import load_iris
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import accuracy_score

iris = load_iris()

# data preprocessing
df_x = pd.DataFrame(
    data=iris.data,
    columns=iris.feature_names
)

df_y = pd.Series(
    data=iris.target,
    name='target'
)

x_train, x_test, y_train, y_test = train_test_split(
    df_x,
    df_y,
    test_size=0.2,
    random_state=228922
)

scaler = StandardScaler()
x_train_scaled = scaler.fit_transform(x_train)
x_test_scaled = scaler.transform(x_test)

# K-Nearest-Neighbors (KNN)
knn = KNeighborsClassifier(n_neighbors=3)
knn.fit(x_train_scaled, y_train)
knn_predict = knn.predict(x_test_scaled)
knn_accuracy = accuracy_score(y_test, knn_predict)
print(f"KNN's ACCURACY           : {knn_accuracy:.10f}")

# Decision Tree
dtree = DecisionTreeClassifier(random_state=228922)
dtree.fit(x_train_scaled, y_train)
dtree_predict = dtree.predict(x_test_scaled)
dtree_accuracy = accuracy_score(y_test, dtree_predict)
print(f"DECISION TREE's ACCURACY : {dtree_accuracy:.10f}")