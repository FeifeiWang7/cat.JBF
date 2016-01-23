/* a1 < a2 < ... < an */
/*
        insertion       merge   heapsort        quicksort
        n^2             nlgn    nlgn            worst n^2 average nlgn
        in place        not     in place        in place
*/
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
////////
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
//////
void max_heapify(int *a, int len, int i)
{
        int largest = i, left = 2*i+1, right = 2*i+2, tmp;
        if((left < len) && (a[left] > a[largest])) largest = left;
        if((right < len) && (a[right] > a[largest])) largest = right;
        if(i != largest)
        {
                tmp = a[largest];
                a[largest] = a[i];
                a[i] = tmp;
                max_heapify(a, len, largest);
        }
}
void build_max_heap(int a[], int len)
{
        for(int i = len-1; i >= 0; i--) max_heapify(a, len, i);
}
int *HeapSort(int a[], int len) //nlgn, in place
{
        build_max_heap(a, len);
        int i, tmp;
        for(i = len-1; i>0; i--)
        {
                tmp = a[0];
                a[0] = a[i];
                a[i] = tmp;
                max_heapify(a, i, 0);
        }
        return a;
}
///////
int *QuickSort(int a[], int left, int right) //nlgn ~ n^2, in place
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
    return a;
}
