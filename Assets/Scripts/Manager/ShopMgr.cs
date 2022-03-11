using UnityEngine;
using UnityEngine.UI;


public class ShopMgr : MonoBehaviour
{
    [SerializeField] private GameObject _commoditys;
    [SerializeField] private GameObject _commodityPrefab;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private DataSo _dataSo;
    public void OpenShop(int x,int y)
    {
        _shopPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
