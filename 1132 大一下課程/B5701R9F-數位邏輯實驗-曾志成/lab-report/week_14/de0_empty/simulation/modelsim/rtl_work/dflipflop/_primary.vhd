library verilog;
use verilog.vl_types.all;
entity dflipflop is
    port(
        d               : in     vl_logic;
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        q               : out    vl_logic;
        q_n             : out    vl_logic
    );
end dflipflop;
