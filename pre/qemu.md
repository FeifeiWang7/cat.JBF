##### install qemu------
If encountering "ERROR: glib-2.12 gthread-2.0 is required to compile QEMU":
sudo apt-get install libglib2.0-dev zlib1g-dev
To use the autogen script:
sudo apt-get install autoconf

./configure --enable-kvm --enable-debug --enable-vnc --enable-werror --target-list="x86_64-softmmu"

- configure is used to generate Makefile, for more information, use ./configure --help
--enable-kvm: compile KVM modul, so that QEMU can use KVM to access hardware virtualization
--enable-vnc: enable vnc
--enalbe-werror: treat warnings as errors when compiling
--target-list: choose target architecture for faster compilation because by default, it compiles all architecture

From the configuration, if SDL is not supported, install it, otherwise I will stuck at "VNC server running on `127.0.0.1:5900'"... 
sudo apt-get install libsdl-dev

sudo make 
sudo make install

##### install KVM ------
check if CPU supports hardware virtualization
grep -E 'vmx|svm' /proc/cpuinfo
If there is output "vmx", show the face :)

check if kvm module is loaded
lsmod | grep kvm
If there is output like "kvm_intel 143187 0" and "kvm 455835 1 kvm_intel", show the face :D

##### basic knowledge ------
Each VM is a process in the host, and vCPU in the VM is a thread in the qemu process.

##### create mirror VM ------
Mirror is like the disk of VM, so I need to create mirror before starts VM
qemu-img create -f qcow2 xxx.img 10G
/* -f specifies the format of the image, and qcow2 is most commonly used by qemu;
xxx.img is the name of the image;
10G is the size of the image.*/

##### start VM ------
qemu-system-x86_64 xxx.img
qemu-system-x86 xxx.img

This should not boot because I haven't installed OS for the VM

##### create OS image ------
Download xxx.iso

qemu-system-x86_64 -m 2048 -enable-kvm experiment.img -cdrom ~/Downloads/ubuntu-14.04.1-desktop-amd64.iso
/* -m specifies the size of memory in VM in MB;
-enable-kvm specifies acceleration with KVM;
-cdrom adds iso */

If it reports an error that device is busy, it means that either virtualbox or vmware is running on the same machine. The kernel module of virtualbox or vmware and KVM cannot take advantage of Intel VT-x or AMD-V at the same time. So ... deactivate them.

##### start VM ------
qemu-system-x86_64 -m 2048 -enable-kvm experiment.img

with multiple cores

qemu-system-x86_64 -m 2048 -smp 4 -enable-kvm experiment.img

##### want graphical interface? ------
install virt-manager
sudo virt-manager
