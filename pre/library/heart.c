void heart()
{
  char fei[5000];
	int f = 0;
	char c='*';

	int  i, j, k, l, m;
	for (i=1; i<=3; i++) 
	{
	  for (j=1; j<=32-2*i; j++)
		{
			fei[f] = ' ';
			f++;
		}
		for (k=1; k<=4*i+1; k++)
		{
			fei[f] = '*';
			f++;
		}
		for (l=1; l<=13-4*i; l++)
		{
			fei[f] = ' ';
			f++;
		}
		for (m=1; m<=4*i+1; m++)
		{
			fei[f] = '*';
			f++;
		}
		fei[f] = '\n';
		f++;
	}

	for (i=1; i<=3; i++)
	{
		for (j=1; j<=24+1; j++)
		{
			fei[f] = ' ';
			f++;
		}
		for (k=1; k<=29; k++)
		{
			fei[f] = '*';
			f++;
		}
		fei[f] = '\n';
		f++;
	}

	for (i=7; i>=1; i--)
	{
		for (j=1; j<=40-2*i; j++)
		{
			fei[f] = ' ';
			f++;
		}
		for (k=1; k<=4*i-1; k++)
		{
			fei[f] = '*';
			f++;
		}
		fei[f] = '\n';
		f++;
	}

	for (i=1; i<=39; i++)
	{
		fei[f] = ' ';
		f++;
	}
	fei[f] = '*';
	f++;
	fei[f] = '\n'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;
	fei[f] = '\t'; f++;

	fei[f] = 'G'; f++;
	fei[f] = 'o'; f++;
	fei[f] = 'o'; f++;
	fei[f] = 'd'; f++;
	fei[f] = ' '; f++;
	fei[f] = 'n'; f++;
	fei[f] = 'i'; f++;
	fei[f] = 'g'; f++;
	fei[f] = 'h'; f++;
	fei[f] = 't'; f++;
	fei[f] = ' '; f++;
	fei[f] = 'w'; f++;
	fei[f] = 'o'; f++;
	fei[f] = 'r'; f++;
	fei[f] = 'l'; f++;
	fei[f] = 'd'; f++;
	fei[f] = '!'; f++;

	fei[f] = '\0';
}
