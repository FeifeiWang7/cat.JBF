//for string
size_t BKDRHash(const T *str)
{
        register size_t hash = 0;
        while (size_t ch = (size_t)*str++)
        {
                hash = hash * 131 + ch; // 也可以乘以31、131、1313、13131、131313..
                // 可将乘法分解为位运算及加减法可以提高效率，如将上式表达为：hash = hash << 7 + hash << 1 + hash + ch;
        }
        return hash;
}

unsigned int times33(char *str)
{
    unsigned int val = 0;
    while (*str)
        val = (val << 5) + val + (*str++);
    return val;
}
