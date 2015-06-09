unsigned long get_symbol(char *name) //get kernel function address
{
        FILE *f;
        unsigned long addr;
        char dummy, sym[512];
        int ret = 0;

        f = fopen("/proc/kallsyms", "r");
        if(!f) return 0;

        while(ret != EOF)
        {
                ret = fscanf(f, "%p %c %s\n", (void **)&addr, &dummy, sym);
                if(ret == 0)
                {
                        fscanf(f, "%s\n", sym);
                        continue;
                }
                if(!strcmp(name, sym))
                {
                        printf("[+] resolved symbol %s to %p\n", name, (void*)addr);
                        fclose(f);
                        return addr;
                }
        }
        fclose(f);
        return 0;
}
