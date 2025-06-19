module dflipflop(
    input d,
    input clk,
    input reset,
    output logic q,
    output logic q_n
);
    //正邊緣觸發 D型正反器
    always_ff @(posedge clk)
    begin
			if (reset) begin
                q <= 1'b0;
                q_n <= 1'b1;
			end
			else begin
                q <= d;
                q_n <= ~d;
			end
    end

endmodule