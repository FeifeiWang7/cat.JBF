#### Compile

gcc -o x x.c -lpthread

or

gcc -c x.c  
gcc x.o -o x -lpthread  

or

gcc -pthread -c x.c  
gcc x.o -o x -pthread  

-pthread is equal to 

gcc -D -REENTRANT -lpthread xxx. c

其中-REENTRANT宏使得相关库函数(如stdio.h、errno.h中函数) 是可重入的、线程安全的(thread-safe)，-lpthread则意味着链接库目录下的libpthread.a或libpthread.so文件。

#### Create

pthread_create(pthread_t *thread, const pthread_attr_t *attr, void *(start_routine)(void*), void *arg);

start_routine为新线程的入口函数，arg为传递给start_routine的参数。

void* thread1(void)
{
        int count = 0;
        while(1)
        {
printf("1 - %d\n", count);
                count++;
                if(count == 500)
                {
                        count = 0;
                        sleep(1);
                }
        }
        return NULL;
}
void* thread2(void *t1)
{
        char c;
        while(1)
        {
                scanf("%c",&c);
                printf("%c\n",c);
                if(c == 'e')
                {
                        pthread_cancel(* (pthread_t *)t1);
                        break;
                }
        }
        return NULL;
}


每个线程都有自己的线程ID，以便在进程内区分。线程ID在pthread_create调用时回返给创建线程的调用者；一个线程也可以在创建后使用pthread_self()调用获取自己的线程ID：pthread_self (void) ;

Example:
 * no argument
	pthread_t id1,id2;
	ret = pthread_create(&id2, NULL, (void*)myThread1, NULL);

 * with arguments int *
	int test=4;
	int *attr=&test;
	error=pthread_create(&tidp,NULL,create,(void *)attr);

 * with arguments char *
	char *a="zieckey";
	int error;
	pthread_t tidp;
	error=pthread_create(&tidp, NULL, create, (void *)a);

 * with argument struct
	struct menber *b;
	b=(struct menber *)malloc( sizeof(struct menber) );
	b->a = 4;
	b->s = "zieckey";
	error = pthread_create(&tidp, NULL, create, (void *)b);

#### Exit

线程的退出方式有三种方式：
a、执行完成后隐式退出；
b、由线程本身显示调用pthread_exit 函数退出；pthread_exit (void * retval) ;
c、被其他线程用pthread_cancel函数终止：pthread_cancel (pthread_t thread) ;

在某线程中调用此函数，可以终止由参数thread 指定的线程。
如果一个线程要等待另一个线程的终止，可以使用pthread_join函数，该函数的作用是调用pthread_join的线程将被挂起直到线程ID为参数thread的线程终止：
pthread_join (pthread_t thread, void** threadreturn);

#### Mutex

int x; // 进程中的全局变量  
pthread_mutex_t mutex;  
pthread_mutex_init(&mutex, NULL); //按缺省的属性初始化互斥体变量mutex  
pthread_mutex_lock(&mutex); // 给互斥体变量加锁  
… //对变量x 的操作  
phtread_mutex_unlock(&mutex); // 给互斥体变量解除锁  
