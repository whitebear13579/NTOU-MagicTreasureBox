class AgeNotLegalError(Exception):
    pass

age = -5
if age < 0:
    raise AgeNotLegalError("Age cannot be negative.")

'''
Traceback (most recent call last):
File "/home/main.py", line 6, in <module>
    raise AgeNotLegalError("Age cannot be negative.")
AgeNotLegalError: Age cannot be negative.
'''