library verilog;
use verilog.vl_types.all;
entity day_timer is
    port(
        clk             : in     vl_logic;
        reset           : in     vl_logic;
        sec_tens        : out    vl_logic_vector(2 downto 0);
        min_tens        : out    vl_logic_vector(2 downto 0);
        sec_units       : out    vl_logic_vector(3 downto 0);
        min_units       : out    vl_logic_vector(3 downto 0)
    );
end day_timer;
