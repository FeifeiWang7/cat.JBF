#### match kernel version

On the phone

	cat /proc/version
	
On kernel image

	strings kernel.Image  | grep "Linux ver"

Sample output

	Linux version 3.10.30-00218-g13d6847-dirty (android@localhost) (gcc version 4.7 (GCC) ) #1 SMP PREEMPT Mon Nov 17 16:54:01 CST 2014

Check if "3.10.30-00218-g13d6847-dirty" matches

