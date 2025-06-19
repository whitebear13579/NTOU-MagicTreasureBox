vsim -voptargs=+acc work.tb_updown
view structure wave signals

do wave.do

log -r *
run -all

