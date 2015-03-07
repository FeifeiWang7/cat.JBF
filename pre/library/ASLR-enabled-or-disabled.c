//ASLR is enabled via /proc/sys/kernel/randomize_va_space

unsigned long long getrsp()
{
  __asm__ ("movq %rsp, %rax");
}
 
int main()
{
  printf("%llx\n", getrsp());
  return 0;
}

//if the output is different each time, ASLR is enabled
//if the output is the same each time, ASLR is disabled
