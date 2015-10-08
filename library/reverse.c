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

#define INT_MAX 2147483647
#define INT_MIN -2147483648
int reverseInteger(int x) {
    long long out=0;
    int sign = x >= 0 ? 1:-1;
    x=abs(x);
    while(x>0){
        out = out * 10 + x % 10;
        if(out > INT_MAX || out*sign < INT_MIN)
            return 0;
        x /= 10;
    }
    return out*sign;
}
