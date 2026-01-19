import matplotlib.pyplot as plt
import yfinance as yf
import pandas as pd

df = yf.download('NVDA', start="2025-01-01", end="2025-12-01", auto_adjust=True)
date = df.index
close_price = df['Close'].squeeze()
volume = df['Volume'].squeeze()
fig, (ax1, ax2) = plt.subplots(2, 1, sharex=True)
ax1.plot(date, close_price, label="Close Price")
ax1.set_ylabel('Close Price')
ax1.legend()
ax2.bar(date, volume)
ax2.set_ylabel('Volume')

plt.xlabel('Date')
plt.suptitle('NVDA Price and Volume', fontweight='bold')

fig.autofmt_xdate()
plt.tight_layout()
plt.show()