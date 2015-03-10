### Compile Kernel

	sudo apt-get install build-essential libncurses5 libncurses5-dev

	make menuconfig

	make -j4 

4 is the number of cores available on the system

	make modules

	sudo make modules_install

Copy kernel modules to /lib/modules/kernel-version

	sudo make install

Copy the kernel to /boot and naming it /boot/vmlinuz, copying the privious kernel to /boot/vmlinuz.old, install System.map-kernel-version, config-kernel-version, vmlinuz-kernel-version to /boot directory

	sudo update-initramfs -c -k kernel-version 

Create initramfs for a specific kernel, -c create, -k specify version

	sudo vim /etc/default/grub
	GRUB_DEFAULT=10
	#GRUB_HIDDEN_TIMEOUT=0
	#GRUB_HIDDEN_TIMEOUT_QUIET=true
	GRUB_TIMEOUT=20

	sudo update-grub2

#### Meaning of .config

- General Setup -----
Support for paging of anonymous memory (swap) - enable swap space on the system. When RAM is not enough, the Linux kernel will move out old pages of memory to the swap space.
System V IPC - inter process communication, allows programs to use message queues, semaphores and shared memory segments.
RCU Subsystem - RCU (Read Copy Update) is a synchronisation primitive supported by the Linux kernel which offers fast access to shared resources (programming-terms) in case of a many-read and infrequent write access behaviour. If there are multiple cores or processors in the system, it is wise to enable it. Otherwise, set it to UP-kernel (UniProcessor).
Kernel .config support - building in .config support allows users to obtain the configuration for a running kernel from the kernel itself. 
	Enable access to .config through /proc/config.gz - the subselection to support /proc/config.gz is an easy-to-use interface to the kernel configuration of a running kernel: extract /proc/config.gz (for instance, zcat /proc/config.gz > /usr/src/linux/.config and you have this kernel's configuration at hand.

- Enable Loadable Module Support -----
Enable loadble module support
	Module unloading

- Enable the Block Layer -----
Enable the block layer - use block devices or Linux kernel components that use functions from the block layer, e.g., SCSI or SCSI emulating devices, the ext3 file system or USB storage.
	IO Schedulers - controls how and when the kernel writes or reads data to/from disks. For desktop systems, CFQ scheduler is a good choice.

- Processor Type and Features -----
Tickless System (Dynamic Ticks) - Unless you need the shortest latency possible, using dynamic ticks will ensure that timer interrupts only fire when needed.
High Resolution Timer Support - Most relatively modern systems (Pentium III and higher) have high resolution timers, allowing for more precise timing. Not really mandatory, but some applications like mplayer can benefit from using hi-res timers.
Symmetric multi-processing support - If you have multiple (identical) CPUs or your CPU has multiple cores, enable this.
Single-depth WCHAN output - WCHAN is the abbreviation for "waiting channel" and identifies where tasks are currently waiting for. With this enabled, the calculation for the waiting channel is simplified at the expense of accuracy. Most users don't need this level of accuracy and the simplifications means less scheduling overhead.
Disable bootmem code - This optimizes some complex initial memory allocation fragments within the Linux kernel.
Processor family - select CPU type (see the /proc/cpuinfo information).
SMT (Hyperthreading) scheduler support - This should be selected if you have a modern Pentium chip with hyperthreading support. It is not mandatory though (the kernel will run fine without it) but might improve scheduling decisions made by the kernel.
HPET Timer Support - This enables support for the High Precision Event Timer, which can be seen as a time-source resource on somewhat more modern systems. Especially if you have more than 1 core/CPU, enabling this offers "cheaper" time access than without HPET Timer support.
Multi-core scheduler support - improve the CPU scheduler performance if there are multiple cores.
Preemption Model (Preemptible Kernel (Low-Latency Desktop)) - Preemption means that a priority process, even when currently in kernel mode executing a system call, can yield his CPU time to another process. The user will notice this as if his system is running somewhat more 'smoothly' as applications might react faster to user input.
	No Forced Preemption
	Voluntary Kernel Preemption - low-priority processes can voluntarily yield CPU time
	Preemptible Kernel - all processes can yield CPU time (as long as they're not in a critical kernel region at that moment)
Machine Check / overheating reporting - MCE allows the processor to notify the kernel when problems are detected (like overheating); based on its severity, the Linux kernel can report the issue or take immediate action.
Intel MCE features - This is part of the "Machine Check / overheating reporting" section, and enables Intel-specific MCE features. I enable this, as I have an Intel-based system.
Memory Model (Sparse Memory) - If you have a 32-bit processor, selecting Flat Memory is what you need. CPUs with a larger address space support (like 64-bit CPUs) most likely only allow you to select "Sparse Memory" as you are not likely to have more than a few thousand terabytes of RAM ;-) When "Sparse Memory" is selected, "Sparse Memory virtual memmap" should be selected as well.
MTRR (Memory Type Range Register) support - With MTRR support, applications such as the X server can control how the processor caches memory accesses, boosting performance for reads/writes to certain memory ranges.
Enable seccomp to safely compute untrusted bytecode - enable this in case an application might want to use it. It has no impact if no such applications exist on the system, and if they do, you most likely want the added security measures this provides.

- Power Management and ACPI Options -----
The power management options provide power-saving features for Linux, not only the APM / ACPI support, but also suspend-to-ram and standby support.

Power Management Support - Enable this to be able to select one or more of the other power management options.
Suspend to RAM and standby - If you will have moments where you temporarily leave your system but don't want to shut it down and boot it back later, you can opt to have the system suspend itself into memory - in this case, many powerconsuming devices are shut down but you don't lose any information as everything remains in memory (and memory remains powered up).
Hibernation (aka 'suspend to disk') - In hibernation, all devices shut down. The current state of system (such as your memory content) is saved into your swap space. When you boot your system back, the Linux kernel will detect this in the swap space and load all information back into memory so you can continue where you left off.
	With suspend to disk enabled, set the default resume partition to your swap partition.
ACPI (Advanced Configuration and Power Interface) Support - Within this section you can configure several aspects of the ACPI support. Enabling ACPI can be of great help to reduce power consumption as it is a powerful technology. Sadly, not every device follows the ACPI guidelines strictly. You will find topics on the internet where boot failures or network irregularities can be solved by disabling a part of the ACPI support inside Linux.
	Within the ACPI configuration you should select the components for which you want support. On regular desktops, you most likely don't have a battery so support for that (and AC Adapters) won't be necessary.
CPU Frequency Scaling - If you own a laptop you'll most likely want to enable CPU Frequency scaling as it will slow down the CPU speed (and the power consumption with it) when the CPU isn't used.

- Bus options (PCI etc.) -----
A bus is a physical connection between several devices. The most popular bus technology within a computer nowadays is PCI (or PCI Express) but a few other bus technologies exist (for instance PCMCIA).

- Executable File Formats/Emulations -----
Select what binaries (format for executable files with machine instructions inside) Linux should support.

The binary format used by Linux is ELF. Very old Linux systems and a couple of BSD operating systems use a.out binaries but it isn't necessary to include support for those any more. If you are configuring for a 64-bit system, definitely enable IA32 Emulation.

- Networking -----
Within the 'Networking options', you will need to enable support for the networking technologies (not hardware) you want to support.

Packet socket - This allows programs to interface with the network devices immediately (without going through the network protocol implementation on the Linux kernel). It is required by tools such as tcpdump / wireshark (popular network analysing tools). You don't need to enable this, but I often perform network analysis myself so I need to have this enabled.
Unix domain sockets - Sockets are a standard mechanism in Unix for processes to communicate with each other. This is an important setting that you must leave on.
TCP/IP networking - Although you don't have to select any of the subfeatures that are shown when you enable this, TCP/IP networking support is definitely a must-have.
Network packet filtering framework (Netfilter) - Enable this if you are planning on configuring a firewall on your system or have your system act as a gateway for others. Enable the 'IP tables support' found under 'IP: Netfilter Configuration' and select all.
Users of a wireless network card will, under 'Networking', also select the Wireless configuration.
I've selected these options because IEEE 802.11 is the standard for wireless networking:
	cfg80211 - wireless configuration API You need to enable this if you have a wireless card
	enable powersave by default - enables powersaving features of the wireless cards - definitely a must-have if you have wireless on a laptop as this reduces power consumption dramatically.

- Device Drivers -----
Blcok devices - devices to access data in blocks. 
	Enable loopback device support - allows to mount images (files) just like they were devices.
SCSI device support - for system that has SCSI or SATA (Serial ATA)
Enable ATA ACPI Support
Enable Intel ESB, ICH, PIIX3, PIIX4 PATA/SATA support
Network device support 
Dummy net driver support - This driver allows me to create an interface which takes on all packets and just ignores them. This seems to be a weird driver, but it can come in handy from time to time. Also, this has no impact on my kernel size so I don't mind enabling this for the few times I actually use it.
Ethernet (1000 Mbit) - I have a Realtek 8169 ethernet card (which is a 1Gbit network card) as mentioned by lspci | grep Ethernet
Wireless LAN - As my system is a laptop with onboard wireless network card, I need to enable WLAN support as well.
Wireless LAN (IEEE 802.11)

- Other Config -----
CONFIG_SLUB setting can easily share the objects which is the same size and other purpose.

### Use old config

Rebuilding the kernel

when want to upgrade the kernel, there is no need to config it again.

cd /usr/src/linux

Load current kernel configuration

zcat /proc/config.gz > .config

make oldconfig

make

choose initramfs over initrd.

Initramfs is used as the first root filesystem that your machine has access to. It is used for mounting the real rootfs which has all your data. The initramfs carries the modules needed for mounting your rootfs. But you could always compile your kernel to have these modules. Then would you need the initramfs? The answer to this is “depends on your system”. Some system configurations need a user space utility to provoke the kernel to configure the devices appropriately. Eg: cryptdevices : they need to have a password from the user. This password requesting utility being a user space utility, could pose a chicken and egg problem i.e your rootfs contains the user space utilities, but the rootfs cannot come up till the user space utilities are available. In such cases, the initramfs plays a mediator in between giving a temporary rootfs which has bears the user space utilities needed for mounting the real rootfs.

