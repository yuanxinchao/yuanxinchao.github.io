using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolManager : MonoBehaviour {
    int Capacity = 100;
    private List<GameObject> dormantObjects = new List<GameObject>();
    public GameObject Spawn(GameObject go)
    {
        GameObject temp = null;
        if (dormantObjects.Count > 0)
        {
            foreach(GameObject dob in dormantObjects)
            {
                if(dob.name == go.name)
                {
                    temp = dob;
                    dormantObjects.Remove(temp);
                    return temp;
                }
            }
        }
        temp = GameObject.Instantiate(go) as GameObject;
        temp.name = go.name;
        return temp;
    }

    public void Despawn(GameObject go)
    {
        go.transform.parent = transform;
        go.SetActive(false);
        dormantObjects.Add(go);
        Trim();
    }
    public void Trim()
    {
        while (dormantObjects.Count > Capacity)
        {
            GameObject dob = dormantObjects[0];
            dormantObjects.RemoveAt(0);
            Destroy(dob);

        }


    }
}
