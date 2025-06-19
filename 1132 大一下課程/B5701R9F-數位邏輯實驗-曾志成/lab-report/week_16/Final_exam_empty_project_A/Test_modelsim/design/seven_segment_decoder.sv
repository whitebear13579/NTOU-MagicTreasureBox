//HW2 - seven_segment_decoder main code
module seven_segment_decoder(
	input logic [3:0] a,
	output logic [6:0] seg
);
	//--------------- Method 1 : use assign ---------------
	//a of seven_segment
	assign seg[0] = ~a[3] & ~a[2] & ~a[1] & a[0] | ~a[3] & a[2] & ~a[1] & a[0] | a[3] & a[2] & ~a[1] & a[0] | a[3] & ~a[2] & a[1] & a[0]; 
	//b of seven_segment
	assign seg[1] = a[3] & a[2] & ~a[1] & ~a[0] | ~a[3] & a[2] & ~a[1] & a[0] | a[3] & a[1] & a[0] | a[2] & a[1] & ~a[0];
	//c of seven_segment
	assign seg[2] = a[3] & a[2] & a[1] | ~a[3] & ~a[2] & a[1] & ~a[0] | a[3] & a[2] & ~a[1] & ~a[0];
	//d of seven_segment
	assign seg[3] = a[3] & ~a[2] & a[1] & ~a[0] | ~a[3] & ~a[2] & ~a[1] & a[0] | ~a[3] & a[2] & ~a[1] & ~a[0] | a[2] & a[1] & a[0];
	//e of seven_segment
	assign seg[4] = ~a[3] & a[2] & ~a[1] | ~a[2] & ~a[1] & a[0] | ~a[3] & a[0];
	//f of seven_segment
	assign seg[5] = a[3] & a[2] & ~a[1] & a[0] | ~a[3] & a[1] & a[0] | ~a[3] & ~a[2] & a[1] | ~a[3] & ~a[2] & a[0];
	//g of seven_segment 
	assign seg[6] = ~a[3] & ~a[2] & ~a[1] | a[3] & a[2] & ~a[1] & ~a[0] | ~a[3] & a[2] & a[1] & a[0];
	
	//--------------- Method 2 : use case ---------------
	/*
	always_comb
	begin
		case(a)
			4'b0000: seg = 7'b1000000; //0
			4'b0001: seg = 7'b1111001; //1
			4'b0010: seg = 7'b0100100; //2
			4'b0011: seg = 7'b0110000; //3
			4'b0100: seg = 7'b0011001; //4
			4'b0101: seg = 7'b0010010; //5
			4'b0110: seg = 7'b0000010; //6
			4'b0111: seg = 7'b1111000; //7
			4'b1000: seg = 7'b0000000; //8
			4'b1001: seg = 7'b0010000; //9
			4'b1010: seg = 7'b0001000; //10(A)
			4'b1011: seg = 7'b0000011; //11(B)
			4'b1100: seg = 7'b1000110; //12(C)
			4'b1101: seg = 7'b0100001; //13(D)
			4'b1110: seg = 7'b0000110; //14(E)
			4'b1111: seg = 7'b0001110; //15(F)
		endcase
	end*/

endmodule
