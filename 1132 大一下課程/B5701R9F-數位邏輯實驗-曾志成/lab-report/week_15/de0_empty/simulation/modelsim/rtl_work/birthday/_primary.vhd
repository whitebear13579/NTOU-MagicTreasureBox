library verilog;
use verilog.vl_types.all;
entity birthday is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        date            : out    vl_logic_vector(3 downto 0)
    );
end birthday;
