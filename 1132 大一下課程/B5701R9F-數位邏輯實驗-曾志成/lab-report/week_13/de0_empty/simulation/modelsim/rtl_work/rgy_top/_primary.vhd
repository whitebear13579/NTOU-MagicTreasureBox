library verilog;
use verilog.vl_types.all;
entity rgy_top is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        r               : out    vl_logic_vector(1 downto 0);
        g               : out    vl_logic_vector(1 downto 0);
        y               : out    vl_logic_vector(1 downto 0)
    );
end rgy_top;
