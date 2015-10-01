int* createPrime(int n)
{
        int* result = (int *)malloc(sizeof(int)*n);
        result[0] = 1;
        result[1] = 2;
        int i;
        for(i = 2; i < n; i++)
        {
                int j;
                for(j = result[i-1] + 1; j <= n; j++)
                {
                        int k;
                        for(k = 1; k < i; k++)
                        {
                                if( j % result[k] == 0) break;
                        }
                        if(k == i)
                        {
                                result[i] = j;
                                break;
                        }
                }
        }
        return result;
}
