vsim -voptargs=+acc work.tb_timer2
view structure wave signals

do wave.do

log -r *

add wave /tb_timer2/u_day_timer/sec_unit_carry
add wave /tb_timer2/u_day_timer/sec_ten_carry
add wave /tb_timer2/u_day_timer/min_unit_carry
add wave /tb_timer2/u_day_timer/min_ten_carry

run -all

