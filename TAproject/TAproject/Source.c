#include <stdio.h>

void main()
{
	char array1[4];// = { '7', '1', '5', '0' };
	int integer = 10;

	array1[3] = (char) 0;
	array1[2] = (char) 5;
	array1[1] = (char) 1;
	array1[0] = (char) 7;

	printf("%d\n\n", array1);

	printf("%d\n\n", integer);

	printf("hello world");

	getch();
}