transcript on
if {[file exists rtl_work]} {
	vdel -lib rtl_work -all
}
vlib rtl_work
vmap work rtl_work

vlog -sv -work work +incdir+C:/Users/user/Desktop/de0/design {C:/Users/user/Desktop/de0/design/sub_8bit.sv}
vlog -sv -work work +incdir+C:/Users/user/Desktop/de0/design {C:/Users/user/Desktop/de0/design/mcu.sv}
vlog -sv -work work +incdir+C:/Users/user/Desktop/de0/design {C:/Users/user/Desktop/de0/design/adder_8bit.sv}

