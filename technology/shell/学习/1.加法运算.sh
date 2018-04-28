#!/bin/bash
echo hello;
a=1;
for files in `ls`
do
	a=`expr $a + 1`;
	if [ $a -gt 59 ]
	then echo "finish"
	break
	fi
done