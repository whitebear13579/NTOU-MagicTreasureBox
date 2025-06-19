vsim -voptargs=+acc work.tb_studentID
view structure wave signals

do wave.do

log -r *
run -all

