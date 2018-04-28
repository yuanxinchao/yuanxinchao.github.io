#!/bin/bash
echo hello;
a=1;
c=2;
hname=".jpg"
for files in `ls *.jpg`
do
	a=${files:0:5}
	c=${files:4:1}
	c=`expr $c + 1`
	echo $c
	echo $files $a$c$hname
	mv $files $a$c$hname
done