import re


class FileVisitor:
    def __init__(self, fileName):
        self.file = open(fileName, "r+", encoding='UTF-8')

    def Regex(self, regex):
        txt = self.file.read()
        ret = re.findall(regex, txt, re.MULTILINE)
        return ret
