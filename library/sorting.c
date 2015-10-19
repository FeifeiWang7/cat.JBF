/* a1 < a2 < ... < an */

int *InsertionSort(int a[], int len) // On2, inplace
{
        /* insert a[i] into the sorted array a[0...i-1]  */
        int i, j, key;
        for(j = 0; j < len; j++)
        {
                key = a[j];
                i = j - 1;
                while((i >= 0) && (a[i] > key))
                {
                        a[i+1] = a[i];
                        i--;
                }
                a[i+1] = key;
        }
        return a;
}

int QuickSort(int a[], int left, int right)
{
    if (left < right)
    {
        int i = left, j = right, p = a[i];
        while (i < j)
        {
            while (i < j && p < a[j])
                j--;
            if (i < j)
                a[i++] = a[j];

            while (i < j && p > a[i])
                i++;
            if (i < j)
                a[j--] = a[i];
        }
        a[i] = p;

        QuickSort(a, left, i - 1);
        QuickSort(a, i + 1, right);
    }
}
