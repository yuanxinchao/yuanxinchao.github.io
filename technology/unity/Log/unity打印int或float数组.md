## unity打印int或float数组

    void Print(float[] num)
    {
        string str;
        string[] nums = num.Select((x) => x.ToString("F1")).ToArray();
        str = String.Join("\n", nums);
        Debug.Log(str);
    }