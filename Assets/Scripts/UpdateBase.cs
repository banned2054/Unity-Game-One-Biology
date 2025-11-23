using Manager;
using UnityEngine;

public class UpdateBase : MonoBehaviour
{
    private void Start()
    {
        var timeMgr = GameObject.Find("Events").GetComponent<TimeMgr>();
        timeMgr.updateList.Add(this);
    }

    public virtual void UpdateTime()
    {
        Debug.Log("base");
    }
}
