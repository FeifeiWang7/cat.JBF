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

#### Section

PLT (Procedure Linkage Table) which is, used to call external procedures/functions whose address isn't known in the time of linking, and is left to be resolved by the dynamic linker at run time.

GOT (Global Offsets Table) and is similarly used to resolve addresses.
