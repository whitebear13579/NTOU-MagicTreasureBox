vsim -voptargs=+acc work.tb_fourbcounter
view structure wave signals

do wave.do

log -r *
run -all

