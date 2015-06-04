int string_to_int(char num[])
{
        //consider integer overflow. Below assume 32-bit integer
        if((num[0] == '-') && (strlen(num) == strlen("-2147483648")) && (strcmp (num, "-2147483648") > 0)) return 0;
        else if( (strlen(num) == strlen("2147483647")) && (strcmp(num, "2147483647") > 0)) return 0;
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
