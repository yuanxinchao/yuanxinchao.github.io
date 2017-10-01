using System.Collections;
using Newtonsoft.Json;

public class Json
{
    private static readonly ArrayList _arrayList = new ArrayList(32);
    public static string ToJsonString(params object[] objs)
    {
        _arrayList.Clear();
        for (int i = 0; i < objs.Length; ++i)
        {
            _arrayList.Add(objs[i]);
        }
        return JsonConvert.SerializeObject(_arrayList);
    }
    public static T ToObject<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}