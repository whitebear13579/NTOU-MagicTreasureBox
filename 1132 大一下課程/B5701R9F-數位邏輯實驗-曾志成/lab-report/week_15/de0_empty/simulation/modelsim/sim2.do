vsim -voptargs=+acc work.tb_birthday
view structure wave signals

do wave.do

log -r *
run -all

