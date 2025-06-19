vsim -voptargs=+acc work.tb_timer
view structure wave signals

do wave.do

log -r *

add wave /tb_timer/u_timer/sec_unit_carry
add wave /tb_timer/u_timer/sec_ten_carry
add wave /tb_timer/u_timer/min_unit_carry
add wave /tb_timer/u_timer/min_ten_carry

run -all

