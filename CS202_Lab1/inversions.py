'''
This is a python program for counting the number of 
inversions in an array in O(n log n) time
'''

def merge(a, b):
    '''
    This function merges two given arrays and counts the number of inversions 
    relative to the two.
    '''
    inv = 0
    c = []
    i, j, n1, n2 = 0, 0, len(a), len(b)
    while i<n1 and j<n2:
        if a[i]<=b[j]:
            c.append(a[i])
            i += 1
        else:
            c.append(b[j])
            j += 1
            inv += n1-i
    while i<n1:
        c.append(a[i])
        i += 1
    while j<n2:
        c.append(b[j])
        j += 1
    return inv, c

def msort(x):
    '''
    This is the divide and conquer algorithm used to sort the array and 
    provide the total number of inversions.
    '''
    n = len(x)
    a, b = x[:n/2], x[n/2:]
    inv1, a = msort(a)
    inv2, b = msort(b)
    inv, c = merge(a, b)
    return inv1+inv2+inv, c

arr = [12, 13, 14, 15, 16, 17, 18, 19, 20, 21]
arr2 = [12, 13, 14, 16, 15, 17, 18, 19, 20, 21]
arr3 = [21, 13, 14, 16, 15, 17, 18, 19, 20, 12]

print("Inversions in arr: ", msort(arr))
print("Inversions in arr2: ", msort(arr2))
print("Inversions in arr3: ", msort(arr3))
