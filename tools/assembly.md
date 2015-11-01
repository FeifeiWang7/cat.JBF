objdump -Dl -Sl a.out

#### AT&T Addressing

format:

segreg: base_address (offset_address, index, size)

which means:

segreg: base_address + offset_address + index * size

Example:

movl %eax, label1(, $2, $4)

movl %ebx, (label2, $2,)

movl %ecx, (%esp)
