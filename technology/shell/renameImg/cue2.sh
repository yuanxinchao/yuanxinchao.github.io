#!/bin/bash
echo hello;
a=1;
c=2;
hname=".jpg"
for files in `ls *.jpg`
do
	a=${files:0:4}
	c=${files:5:1}
	echo $files $a$c$hname
	mv $files $a$c$hname
done