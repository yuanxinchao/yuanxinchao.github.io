import subprocess

path = "E:/working/Pythontest/ffmpeg-20191215-9fe0790-win64-static/ffmpeg-20191215-9fe0790-win64-static/bin/";
command = path + "ffmpeg -i test.mp4 output_audio.wav"

subprocess.call(command, shell=True)