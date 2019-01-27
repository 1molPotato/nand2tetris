// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop local 0
	@0
	D=A
	@LCL
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// label $LOOP_START declaration
($LOOP_START)
// push argument 0
	@0
	D=A
	@ARG
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push local 0
	@0
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// pop local 0
	@0
	D=A
	@LCL
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push argument 0
	@0
	D=A
	@ARG
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 1
	@1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// sub
	@SP
	A=M-1
	M=-M
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// pop argument 0
	@0
	D=A
	@ARG
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push argument 0
	@0
	D=A
	@ARG
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// if pop is true, jump to $LOOP_START
	@SP
	AM=M-1
	D=M
	@$LOOP_START
	D;JNE
// push local 0
	@0
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
