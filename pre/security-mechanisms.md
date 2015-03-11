### mmap_min_addr

To do the null pointer dereference attack, I need to map some code to the first page (addr 0x0), so I have to disable the mmap_min_addr protection.

    sudo echo "vm.mmap_min_addr = 0" >> /etc/sysctl.conf
Make it working

    sudo /sbin/sysctl -p
Check it is applied

    sudo /sbin/sysctl vm.mmap_min_addr
