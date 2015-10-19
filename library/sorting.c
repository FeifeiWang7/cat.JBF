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

void Merge(int a[], int low, int mid, int high)
{
        int n1 = mid-low+1;
        int n2 = high-mid;
        int m1[n1], m2[n2];
        int i,j,k;
        for(i = 0; i < n1; i ++) m1[i] = a[low+i];
        for(j = 0; j < n2; j ++) m2[j] = a[mid+1+j];
        i = 0; j = 0;
        for(k = low; k <= high; k++)
        {
                if(i >= n1) a[k] = m2[j++];
                else if (j >= n2) a[k] = m1[i++];
                else if(m1[i] < m2[j]) a[k] = m1[i++];
                else a[k] = m2[j++];
        }
}
int *MergeSort(int a[], int low, int high) // nlgn, not in place
{
        int mid;
        if(low < high)
        {
                mid = (low + high)/2;
                MergeSort(a, low, mid);
                MergeSort(a, mid+1, high);
                Merge(a, low, mid, high);
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
