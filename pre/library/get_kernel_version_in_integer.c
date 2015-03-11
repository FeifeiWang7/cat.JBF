int get_kern_version(void) // return something like 2600 for kernel 2.6.0, 2619 for kernel 2.6.19 ...
{
    struct utsname buf;
    char second[2],third[3];
    int version = 2000;
    if(uname(&buf) < 0)
            error("can't have ur k3rn3l version. this box isn't for today :P\n");
            
    sprintf(second, "%c", buf.release[2]);
    second[1] = 0;
    version += atoi(second) * 100;
    
    third[0] = buf.release[4];
    if(buf.release[5] >= '0' || buf.release[5] <= '9')
    {
            third[1] = buf.release[5];
            third[2] = 0;
            version += atoi(third);
    }
    else
    {
            third[1] = 0;
            version += third[0] - '0';
    }
    printf("\t\t+ Kernel version %i\n", version);
    return version; 
} 
