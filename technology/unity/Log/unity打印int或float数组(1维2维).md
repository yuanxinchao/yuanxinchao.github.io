## unity打印int或float数组
一维

    void Print(float[] num)
    {
        string str;
        string[] nums = num.Select((x) => x.ToString("F1")).ToArray();
        str = String.Join("\n", nums);
        Debug.Log(str);
    }

二维  

    public static void Print(int[,] map)
    {
        string str = string.Empty;
        int i = 0;
        foreach (var grid in map)
        {
            str = str + grid + "\t";
            i++;
            if (i % map.GetLength(0) == 0)
            {
                str = str + "\n";
            }
        }
        Debug.Log(str);
    }
