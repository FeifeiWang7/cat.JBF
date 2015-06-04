int string_to_int(char num[])
{
        int len = strlen(num);
        int result = 0;
        int i = 0;
        if(num[0] == '-') i = 1;
        for(; i < len; i ++)
        {
                result = result * 10 + (num[i] - '0');
        }
        if(num[0] == '-') result = -result;
        return result;
}
