library verilog;
use verilog.vl_types.all;
entity mcu is
    port(
        CLK             : in     vl_logic;
        HEX0            : out    vl_logic_vector(6 downto 0);
        HEX1            : out    vl_logic_vector(6 downto 0);
        HEX2            : out    vl_logic_vector(6 downto 0);
        HEX3            : out    vl_logic_vector(6 downto 0);
        LED             : out    vl_logic_vector(9 downto 0);
        SW              : in     vl_logic_vector(9 downto 0);
        BTN             : in     vl_logic_vector(2 downto 0)
    );
end mcu;
