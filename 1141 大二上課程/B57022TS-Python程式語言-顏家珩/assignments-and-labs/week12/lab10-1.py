import numpy as np
import matplotlib.pyplot as plt
from matplotlib.pyplot import MultipleLocator
from scipy.integrate import quad

# define the function f(x) = e^(-x^2)
def f(x):
    return np.exp(-x**2)

# calculate the integral of f(x) [0,2]
def cal():
    return quad(f,0,2)

# draw the function in [-1,3]
# in graph, highlight the [0,2] area
# mark the integral value on the graph
def draw():
    x_data = np.linspace(-1, 3, 1000)
    y_data = f(x_data)
    plt.plot(x_data, y_data, label='f(x) = e^(-x^2)')
    x_fill = np.linspace(0, 2, 1000)
    y_fill = f(x_fill)
    plt.fill_between(x_fill, y_fill, color='lightblue', alpha=0.5, label=f'The Result of integral: {cal()[0]:.4f}')
    plt.gca().yaxis.set_major_locator(MultipleLocator(0.1))
    plt.title(f"f(x) = e^(-x^2)")
    plt.xlabel('x')
    plt.ylabel('y')
    plt.legend()
    plt.show()

draw()