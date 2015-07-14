### debug kernel code

Print function name and line number to dmesg

    printk(KERN_INFO "%s():%d\n", __func__, __LINE__);

