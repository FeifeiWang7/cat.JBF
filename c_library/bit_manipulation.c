// 判断奇偶
for (i = 0; i < 100; ++i)   //下面程序将输出0到100之间的所有奇数
    if (i & 1)  
        printf("%d ", i);  
putchar('\n');  

//swap a and b without using extra space
void Swap(int &a, int &b)  
{  
    if (a != b)  
    {  
        a ^= b;  
        b ^= a;  
        a ^= b;  
    }  
}  

//3．变换符号 - 变换符号就是正数变成负数，负数变成正数。
//如对于-11和11，可以通过下面的变换方法将-11变成11
//      1111 0101(二进制) –取反-> 0000 1010(二进制) 加1-> 0000 1011(二进制)
//同样可以这样的将11变成-11
//      0000 1011(二进制) –取反-> 1111 0100(二进制) 加1-> 1111 0101(二进制)
//因此变换符号只需要取反后加1即可。完整代码如下：
int SignReversal(int a)  
{  
    return ~a + 1;  
}  

//4．求绝对值
//位操作也可以用来求绝对值，对于负数可以通过对其取反后加1来得到正数。对-6可以这样：
//因此先移位来取符号位，int i = a >> 31;要注意如果a为正数，i等于0，为负数，i等于-1。然后对i进行判断——如果i等于0，直接返回。否之，返回~a+1。完整代码如下：
int my_abs(int a)  
{  
    int i = a >> 31;  
    return i == 0 ? a : (~a + 1);  
}
//现在再分析下。对于任何数，与0异或都会保持不变，与-1即0xFFFFFFFF异或就相当于取反。因此，a与i异或后再减i（因为i为0或-1，所以减i即是要么加0要么加1）也可以得到绝对值。所以可以对上面代码优化下：
int my_abs(int a)  
{  
    int i = a >> 31;  
    return ((a ^ i) - i);  
}
//reverse bits in an integer
unsigned int reverseBits(unsigned int n)
{
        unsigned int i, result = 0;
        for(i = 0; i < 32; i ++)
        {
                int tmp1 = (n >> i) << 31 >> i;
                //int tmp2 = n >> (i+1);
                //int tmp3 = (tmp1 | tmp2) >> i;
                result = result | tmp1;
        }
        return result;
}
uint32_t reverseBits(uint32_t n)
{
   n = ((n<<16) | (n>>16));                         //swap [31:16]<=>[15:0]
   n = ((n&0x00ff00ff)<< 8 | (n&0xff00ff00)>> 8);   //swap [31:24]<=>[23:16] , [15:8]<=>[7:0]
   n = ((n&0x0f0f0f0f)<< 4 | (n&0xf0f0f0f0)>> 4);   //swap ...
   n = ((n&0x33333333)<< 2 | (n&0xcccccccc)>> 2);   //swap ...
   n = ((n&0x55555555)<< 1 | (n&0xaaaaaaaa)>> 1);   //swap ...
   return n;
}
//count how many 1's in an integer
int count(int a)
{
	int n = 0;
	while(a)
	{
		if(a&1) n++;
		a = a >> 1;	
	}
	return n;
}
