/*
* CS5541 Bits Lab (Assignment 2)
*
* <Please put your header information here>
*
* bitslab.c - Source file with your solutions to the Lab.
*          This is the file you will hand in to your instructor.
*/

#if 0
/*
* Instructions to Students:
*
* STEP 1: Read the following instructions carefully.
*/

You will provide your solution to Assignment 2 by
editing the collection of functions in this source file.

INTEGER CODING RULES :

Replace the "return" statement in each function with one
or more lines of C code that implements the function.Your code
must conform to the following style :

int Funct(arg1, arg2, ...) {
	/* brief description of how your implementation works */
	int var1 = Expr1;
	...
		int varM = ExprM;

	varJ = ExprJ;
	...
		varN = ExprN;
	return ExprR;
}

Each "Expr" is an expression using ONLY the following :
1. Integer constants 0 through 255 (0xFF), inclusive.You are
not allowed to use big constants such as 0xffffffff.
2. Function arguments and local variables(no global variables).
3. Unary integer operations !~
4. Binary integer operations & ^ | +<< >>

Some of the problems restrict the set of allowed operators even further.
Each "Expr" may consist of multiple operators.You are not restricted to
one operator per line.

You are expressly forbidden to :
1. Use any control constructs such as if, do, while, for, switch, etc.
2. Define or use any macros.
3. Define any additional functions in this file.
4. Call any functions.
5. Use any other operations, such as &&, || , -, or ? :
6. Use any form of casting.
7. Use any data type other than int.This implies that you
cannot use arrays, structs, or unions.

You may assume that your machine :
1. Uses 2s complement, 32 - bit representations of integers.
2. Performs right shifts arithmetically.
3. Has unpredictable behavior when shifting an integer by more
than the word size.

EXAMPLES OF ACCEPTABLE CODING STYLE :
/*
* pow2plus1 - returns 2^x + 1, where 0 <= x <= 31
*/
int pow2plus1(int x) {
	/* exploit ability of shifts to compute powers of 2 */
	return (1 << x) + 1;
}

/*
* pow2plus4 - returns 2^x + 4, where 0 <= x <= 31
*/
int pow2plus4(int x) {
	/* exploit ability of shifts to compute powers of 2 */
	int result = (1 << x);
	result += 4;
	return result;
}

FLOATING POINT CODING RULES

For the problems that require you to implent floating - point operations,
the coding rules are less strict.You are allowed to use looping and
conditional control.You are allowed to use both ints and unsigneds.
You can use arbitrary integer and unsigned constants.

You are expressly forbidden to :
1. Define or use any macros.
2. Define any additional functions in this file.
3. Call any functions.
4. Use any form of casting.
5. Use any data type other than int or unsigned.This means that you
cannot use arrays, structs, or unions.
6. Use any floating point data types, operations, or constants.


NOTES:
1. Each function has a maximum number of operators(!~& ^ | +<< >> )
that you are allowed to use for your implementation of the function.
Note that '=' is not counted; you may use as many of these as you
want without penalty.
2. The maximum number of ops for each function is given in the
header comment for each function.If there are any inconsistencies
between the maximum ops in the writeup and in this file, consider
this file the authoritative source.

/*
* STEP 2: Modify the following functions according the coding rules.
*/


#endif
/*
* bitAnd - x&y using only ~ and |
*   Example: bitAnd(6, 5) = 4
*   Legal ops: ~ |
*   Max ops: 8
*   Points: 2
*/
int bitAnd(int x, int y) {

	//implementing AND as not(not x OR not y)

	return ~((~x) | (~y));
}
/*
* getByte - Extract byte n from word x
*   Bytes numbered from 0 (LSB) to 3 (MSB)
*   Examples: getByte(0x12345678,1) = 0x56
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 6
*   Points: 2
*/
int getByte(int x, int n) {

	//shifting x right n bytes and reading the LSB by ANDing with 255
	//8 * n is done by shifting n left 3times since 8 = 2^3

	return (x >> (n << 3)) & 0xff;
}
/*
* logicalShift - shift x to the right by n, using a logical shift
*   Can assume that 0 <= n <= 31
*   Examples: logicalShift(0x87654321,4) = 0x08765432
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 20
*   Points: 3
*/
int logicalShift(int x, int n) {

	//shifting x n bits to the right, then getting rid of leading 1s by ANDing the result with a mask with a corresponding number of leading 0s and tailing 1s
	//mask is the same as 0xffff created explicitly by negating a 1 and ORing with a 1 to get all 32bits to be 1 hence ffff (since 0xffff cannot be used directly)


	int y = 0x00000001;
	int mask = ~y;
	mask |= y;
	return ((x >> n) & ~(mask << (32 + ~n + 1)));
}
/*
* bitCount - returns count of number of 1's in word
*   Examples: bitCount(5) = 2, bitCount(7) = 3
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 40
*   Points: 4
*/
int bitCount(int x) {

	/*int c;
	c = (x & 0x55555555) + ((x >> 1) & 0x55555555);
	c = (c & 0x33333333) + ((c >> 2) & 0x33333333);
	c = (c & 0x0F0F0F0F) + ((c >> 4) & 0x0F0F0F0F);
	c = (c & 0x00FF00FF) + ((c >> 8) & 0x00FF00FF);
	c = (c & 0x0000FFFF) + ((c >> 16) & 0x0000FFFF);

	return c;*/

	/* count the bits a byte at a time, keeping the total in each byte of bits
	* then add up the parts of bits */
	int bits = 0;
	int mask = 0x1 | (0x1 << 8) | (0x1 << 16) | (0x1 << 24);
	bits += (x & mask);
	bits += ((x >> 1) & mask);
	bits += ((x >> 2) & mask);
	bits += ((x >> 3) & mask);
	bits += ((x >> 4) & mask);
	bits += ((x >> 5) & mask);
	bits += ((x >> 6) & mask);
	bits += ((x >> 7) & mask);
	return (bits & 0xFF) + ((bits >> 8) & 0xFF) + ((bits >> 16) & 0xFF) + ((bits >> 24) & 0xFF);
}
/*
* bang - Compute !x without using !
*   Examples: bang(3) = 0, bang(0) = 1
*   Legal ops: ~ & ^ | + << >>
*   Max ops: 12
*   Points: 4
*/
int bang(int x) {

	int invx = ~x;                  //if x==0, then -1
	int negx = invx + 1;                //if x==0, then 0
	return ((~negx & invx) >> 31) & 1;  //if x was 0, then MSB is 1, so MSB>>31 & 1 = 1
}
/*
* tmin - return minimum two's complement integer
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 4
*   Points: 1
*/
int tmin(void) {

	return 1 << 31; //10000...
}
/*
* fitsBits - return 1 if x can be represented as an
*  n-bit, two's complement integer.
*   1 <= n <= 32
*   Examples: fitsBits(5,3) = 0, fitsBits(-4,3) = 1
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 15
*   Points: 2
*/
int fitsBits(int x, int n) {

	//return !(((~x & (x >> 0x1F)) + (x & ~(x >> 0x1F))) >> (n + ~0));

	/* see if you shift x by n bits then back by n bits if its still equal */
	/* a ^ b = 0 when a == b*/
	int shift, y;
	shift = 33 + ~n;
	y = (x << shift) >> shift;
	return !(x ^ y);
}
/*
* divpwr2 - Compute x/(2^n), for 0 <= n <= 30
*  Round toward zero
*   Examples: divpwr2(15,1) = 7, divpwr2(-33,4) = -2
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 15
*   Points: 2
*/
int divpwr2(int x, int n) {

	// Something is needed to account for x >> n if positive and x >> n + 1 if negative

	// Subtract 1 from 2^n
	// This accounts for the need to + 1
	int mask = (1 << n) + ~0;

	// Use & operator on mask and sign bit of x 
	int equalizer = (x >> 31) & mask;

	// Adds 1 if x was originally negative
	// Adds 0 if x was originally positive
	return (x + equalizer) >> n;
}
/*
* negate - return -x
*   Example: negate(1) = -1.
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 5
*   Points: 2
*/
int negate(int x) {

	//return ~x + 0x01;

	/* Use the fact that -x = ~x + 1 in twos complement. */
	return (~x) + 1;
}
/*
* isPositive - return 1 if x > 0, return 0 otherwise
*   Example: isPositive(-1) = 0.
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 8
*   Points: 3
*/
int isPositive(int x) {

	//return !((x & 0x80000000) >> 31 | !x);

	/* gets the sign bit, and checks returns 1 if its sign bit is 0 and x != 0*/
	int highbit = ((x & (1 << 31)) >> 31) & 1;
	return (highbit ^ 1) ^ (!x);
}
/*
* isLessOrEqual - if x <= y  then return 1, else return 0
*   Example: isLessOrEqual(4,5) = 1.
*   Legal ops: ! ~ & ^ | + << >>
*   Max ops: 24
*   Points: 3
*/
int isLessOrEqual(int x, int y) {

	/*if x and y have the same sign y plus the complement of x plus one should be positive, if they have different signs the override byte has an effect and sets the result to either one or zero always.*/

	int sign_y, sign_x, comb_sign_xy, override_byte, z;
	sign_y = y >> 31;
	sign_x = x >> 31;
	comb_sign_xy = sign_x^sign_y;
	override_byte = comb_sign_xy&sign_x;
	z = ((y + (~x) + 1)) >> 31;

	return !((z | comb_sign_xy) ^ override_byte);
}
/*
* float_neg - Return bit-level equivalent of expression -f for
*   floating point argument f.
*   Both the argument and result are passed as unsigned int's, but
*   they are to be interpreted as the bit-level representations of
*   single-precision floating point values.
*   Legal ops: Any integer/unsigned operations incl. ||, &&. also if, while
*   Max ops: 10
*   Points: 4
*/
unsigned float_neg(unsigned uf) {

	/* Flips the sign bit if uf isn't inf or NaN */
	unsigned mask = 0x80000000;
	unsigned NaN = 0x7FC00000;
	unsigned inf = 0xFFC00000;
	if (uf == NaN || uf == inf)
		return uf;
	return uf ^ mask;
}
/*
* float_i2f - Return bit-level equivalent of expression (float) x
*   Result is returned as unsigned int, but
*   it is to be interpreted as the bit-level representation of a
*   single-precision floating point values.
*   Legal ops: Any integer/unsigned operations incl. ||, &&. also if, while
*   Max ops: 30
*   Points: 4
*/
unsigned float_i2f(int x) {

	/* squeeze x into 23 bits, rounding following the rounding rules */
	unsigned sign, fraction, exponent = 150, temp, b = 2, top, bottom;
	if (x == 0) return 0;
	if (x == 0x80000000) return 3472883712u;
	sign = (x & 0x80000000);
	fraction = (sign) ? (-x) : (x);

	temp = fraction;
	while (temp & 0xFF000000) {
		/* standard rounding */
		temp = (fraction + (b / 2)) / (b);
		b <<= 1;
		exponent++;
	}
	while (temp <= 0x007FFFFF) {
		temp <<= 1;
		exponent--;
	}
	if (fraction & 0xFF000000) {
		b = 1 << (exponent - 150);

		temp = fraction / b;
		bottom = fraction % b;
		top = b - bottom;

		/* if temp is closer to fraction/b than fraction/b + 1, or its odd,
		round up */
		if ((top < bottom) || ((top == bottom) & temp))
			++temp;

		fraction = temp;
	}
	else {
		while (fraction <= 0x007FFFFF)
			fraction <<= 1;
	}

	return (sign) | (exponent << 23) | (fraction & 0x007FFFFF);
}
/*
* float_twice - Return bit-level equivalent of expression 2*f for
*   floating point argument f.
*   Both the argument and result are passed as unsigned int's, but
*   they are to be interpreted as the bit-level representation of
*   single-precision floating point values.
*   Legal ops: Any integer/unsigned operations incl. ||, &&. also if, while
*   Max ops: 30
*   Points: 4
*/
unsigned float_twice(unsigned uf) {

	/* if its denormalized double fraction, if its normailized, increase
	* exponent, if it on the edege, decrement fraction, increment epx. */
	unsigned expn = (uf >> 23) & 0xFF;
	unsigned sign = uf & 0x80000000;
	unsigned frac = uf & 0x007FFFFF;
	if (expn == 255 || (expn == 0 && frac == 0))
		return uf;

	if (expn) {
		expn++;
	}
	else if (frac == 0x7FFFFF) {
		frac--;
		expn++;
	}
	else {
		frac <<= 1;
	}

	return (sign) | (expn << 23) | (frac);
}

void main() //just checking if my code works. commented out since no function should be defined!
{
	
	printf("shit is real\n");		

	printf("%d\n", bitAnd(6, 5));

	printf("%d\n", getByte(0x12345678, 1));

	printf("%d\n", logicalShift(0x87654321, 4));

	printf("%d\n", bitCount(7));

	printf("%d\n", bang(0));

	printf("%d\n", fitsBits(5,3));

	printf("%d\n", negate(1));

	printf("%d\n", isPositive(-1));

	printf("%d\n", isLessOrEqual(4,5));

	printf("%d\n", divpwr2(-33,4));

	//printf("%d\n", divpwr2(-33, 4));

	//printf("%d\n", divpwr2(-33, 4));

	//printf("%d\n", divpwr2(-33, 4));

	getch();
	
}
