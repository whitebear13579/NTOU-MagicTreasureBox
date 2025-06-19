vsim -voptargs=+acc work.tb_student_num
view structure wave signals

do wave.do

log -r *
run -all

