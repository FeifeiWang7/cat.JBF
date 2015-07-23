#include <signal.h>

void sig_handler(int signo)
{
        if(signo == SIGSEGV)
        {
                printf("Segmentation fault received.\n");
        }
}

int main(void)
{
        if(signal(SIGSEGV, sig_handler) == SIG_ERR)
                printf("cannot catch SIGSEGV.\n");
}
