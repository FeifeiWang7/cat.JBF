char* IntToString(int x, char digit[])
{
        char *p = digit;
        if(x < 0)
        {
                *p++ = '-';
                x = -x;
        }
        int shifter = x;
        do{
                p++;
                shifter = shifter/10;
        }while(shifter);
        *p = '\0';
        do{
                *--p = x%10 + 48;
                x = x/10;
        }while(x);
        return digit;
}
