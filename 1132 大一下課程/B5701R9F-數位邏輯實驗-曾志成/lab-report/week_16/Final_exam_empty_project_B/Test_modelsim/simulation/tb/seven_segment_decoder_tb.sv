// HW2 - seven_segment_decoder 測資
module seven_segment_decoder_tb;

	logic [3:0] a;
	logic [6:0] seg;
	
	seven_segment_decoder decoder(
		.a(a),
		.seg(seg)
	);
	
	initial
	begin
		a = 0;
		#10 a = 1;
		#10 a = 2;
		#10 a = 3;
		#10 a = 4;
		#10 a = 5;
		#10 a = 6;
		#10 a = 7;
		#10 a = 8;
		#10 a = 9;
		#10 a = 10;
		#10 a = 11;
		#10 a = 12;
		#10 a = 13;
		#10 a = 14;
		#10 a = 15;
		#10 $stop;
	end
	
	initial
	begin
		$monitor($time," a=%b, seg=%b",a,seg);
	end

endmodule
