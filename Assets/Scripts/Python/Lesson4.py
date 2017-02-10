from sklearn.datasets import load_iris
from sklearn.neighbors import KNeighborsClassifier

iris = load_iris()
X, y = iris.data, iris.target

classifier = KNeighborsClassifier()

from sklearn.cross_validation import train_test_split

train_X, test_X, train_y, test_y = train_test_split(X, y, train_size=0.5, random_state=1999)
print(y)
print(train_y)
print(test_y)
