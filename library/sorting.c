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
