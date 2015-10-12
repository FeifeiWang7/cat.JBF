int myAtoi(char* num) {
        char *p = num;
        while((*p == ' ') || (*p == '\t') || (*p == '\n')) p++;
        int result = 0;
        int positive = 1;
        if( *p == '-')
        {
                positive = 0;
                p++;
        }
        else if( *p == '+')
        {
                positive = 1;
                p++;
        }
        while(*p >= '0' && *p <= '9')
        {
//              if((tmp >> 31 != result >> 31) || (tmp + '0' - *p)/10 != result)
                if(result > (INT_MAX - *p + '0')/10)
                        return positive?2147483647:-2147483648;
                result = result * 10 + *p - '0';
                p++;
        }
        return positive?result:-result;
}

//another method
#include <limits.h>
#include <ctype.h>
int myAtoi(char* str) {
    long result = 0;
    int sign = 1;
    //discard the first sequence of whitespace characters.
    while(isspace(*str))
    {
        str++;
    }
    if((*str == '+') || (*str == '-'))
    {
        sign = (*str == '+') ? 1:0;
        str++;
    }
    if(!isdigit(*str))
    {
        return 0;
    }
    while(isdigit(*str) && (result <= INT_MAX))
    {
        result = result * 10 + *str - '0' + 0;
        str++;
    }
    if(result > INT_MAX)
    {
        return sign == 1 ? INT_MAX : INT_MIN;
    }
    return sign == 1 ? result : -result;
}
