typedef char stackElementT; //define stack type here

typedef struct
{
	stackElementT *contents;
	int top;
	int maxSize;
}stackT;

void StackInit(stackT *stackP, int maxSize)
{
	stackElementT *newContents;
	newContents = (stackElementT *)malloc(sizeof(stackElementT)*maxSize);
	if(newContents == NULL)
	{
		fprintf(stderr, "Insufficient memory to initialize stack.\n");
		exit(1);
	}
	stackP->contents = newContents;
	stackP->maxSize = maxSize;
	stackP->top = -1;
}

void StackDestroy(stackT *stackP)
{
	free(stackP->contents);
	stackP->contents = NULL;
	stackP->maxSize = 0;
	stackP->top = -1;
}

int StackIsEmpty(stackT *stackP)
{
	return stackP->top < 0;
}

int StackIsFull(stackT *stackP)
{
	return stackP->top >= stackP->maxSize - 1;
}

void StackPush(stackT *stackP, stackElementT element)
{
	if (StackIsFull(stackP))
	{
		fprintf(stderr, "Can't push element on stack: stack is full.\n");
		exit(1);
	}
	stackP->contents[++stackP->top] = element;
}

stackElementT StackPop(stackT *stackP)
{
	if(StackIsEmpty(stackP))
	{
		fprintf(stderr, "Can't pop element from stack: stack is empty.\n");
		exit(1);	
	}
	return stackP->contents[stackP->top--];
}
