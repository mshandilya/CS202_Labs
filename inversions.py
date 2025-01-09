inversions = 0

def merge(a, b):
    c = []
    i, j, n1, n2 = 0, 0, len(a), len(b)
    while i<n1 and j<n2:
        if(a[i]<=b[j]):
            c.append(a[i])
            i += 1
        else:
            c.append(b[j])
            j += 1
            inversions += n1-i
    while i<n1:
        c.append(a[i])
        i += 1
    while j<n2:
        c.append(b[j])
        j += 1
    return c

def msort(x):
    n = len(x)
    a, b = x[:n/2], x[n/2:]
    a, b = msort(a), msort(b)
    return merge(a, b)

arr = [12, 13, 14, 15, 16, 17, 18, 19, 20, 21]
arr2 = [12, 13, 14, 16, 15, 17, 18, 19, 20, 21]
arr3 = [21, 13, 14, 16, 15, 17, 18, 19, 20, 12]

msort(arr)
print("Inversions in arr: ", inversions)
inversions = 0
msort(arr2)
print("Inversions in arr2: ", inversions)
inversions = 0
msort(arr3)
print("Inversions in arr3: ", inversions)
