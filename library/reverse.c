char *reverseString(char *str)
{
        int len = strlen(str);
        int i;
        char *result = (char *)malloc(sizeof(str));
        for(i = 0; i < len; i ++)
                result[i] = str[len - i - 1];
        result[i] = '\0';
        return result;
}
int *reverseArray(int *digits, int digitsSize)
{
        int i;
        int *result = (int *)malloc(digitsSize * sizeof(int));
        for(i = 0; i < digitsSize; i ++)
                result[i] = digits[digitsSize - i - 1];
        return result;
}
