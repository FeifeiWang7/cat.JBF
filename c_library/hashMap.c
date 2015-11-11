#include "uthash.h"
typedef double hashKeyT; /* specify hash key data type */
typedef int    hashValueT;
typedef struct
{
	hashKeyT key; /* key */
	hashValueT value; /* value */
	UT_hash_handle hh;
}hashEntryT;	

void HashInit(hashEntryT **hashMap)
{
	*hashMap = NULL;
}
void HashAdd(hashEntryT **hashMap, hashKeyT key, hashValueT value)
{
	hashEntryT *s;
	HASH_FIND(hh, *hashMap, &key, sizeof(hashKeyT), s);
	if(s == NULL) /* not found */
	{
		s = (hashEntryT *)malloc(sizeof(hashEntryT));
		s -> key = key;
		HASH_ADD(hh, *hashMap, key, sizeof(hashKeyT), s); //key here is the field name in the struct, so must be key, not anything else
	}
	memcpy(&(s -> value), &value, sizeof(hashValueT));
}
hashEntryT *HashFind(hashEntryT **hashMap, hashKeyT key)
{
	hashEntryT *s;
	HASH_FIND(hh, *hashMap, &key, sizeof(hashKeyT), s);
	return s;
}
void HashDelete(hashEntryT **hashMap, hashEntryT *s)
{
	HASH_DEL(*hashMap, s);
	free(s);
}
void HashDeleteAll(hashEntryT **hashMap)
{
	hashEntryT *current, *tmp;
	HASH_ITER(hh, *hashMap, current, tmp)
	{
		HASH_DEL(*hashMap, current);
		free(current);
	}
}
int HashCount(hashEntryT **hashMap)
{
	return HASH_COUNT(*hashMap);
}
void HashClear(hashEntryT **hashMap)
{
	HASH_CLEAR(hh, *hashMap);
}
/*iterating:
	struct hash_map *s;
	for(s = my_hash_table; s!=NULL; s=s->hh.next)
	{
	}
*/
/*HASH_SORT(my_hash_table, sort_func)
int sort_func(void *a, void *b)
{
	if(a<b) return -1;
	if(a == b) return 0;
	if(a>b) return 1;
}
int name_sort(struct my_struct *a, struct my_struct *b) {
    return strcmp(a->name,b->name);
}

int id_sort(struct my_struct *a, struct my_struct *b) {
    return (a->id - b->id);
}

void sort_by_name() {
    HASH_SORT(users, name_sort);
}

void sort_by_id() {
    HASH_SORT(users, id_sort);
}
*/
