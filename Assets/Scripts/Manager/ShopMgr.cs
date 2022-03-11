using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;


public class ShopMgr : MonoBehaviour
{

    [SerializeField] private GameObject _commodityPrefab;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Transform _groundsTransform;
    [SerializeField] private Transform _commoditysTransform;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private DataSo _dataSo;


    private int _commodityPage = 0;
    private int _currentPage = 0;

    public void OpenShop(int x, int y)
    {
        _currentPage = 0;
        _shopPanel.SetActive(true);
        if (_dataSo.CurrentLevel > 3)
        {
            _commodityPage = _dataSo.CurrentLevel / 3;
        }
        else
        {
            _commodityPage = 0;
        }

        ClearPage();
    }

    public void UpPage()
    {
        if (_currentPage == 0) return;
        //清理所有已有商品
        _currentPage--;
        ClearPage();
    }

    public void DownPage()
    {
        if (_currentPage == _commodityPage) return;
        _currentPage++;
        ClearPage();
    }

    public void ClearPage()
    {
        _commoditysTransform.DetachChildren();
        int i = 0;
        int now = 3 * _currentPage;
        for (i = 0; i < 3 && now <= _dataSo.CurrentLevel; i++)
        {
            //实例化新的商品
            GameObject newCommodity = Instantiate(_commodityPrefab, _commoditysTransform).gameObject;
            Image commodityImage = newCommodity.transform.GetChild(0).GetComponent<Image>();
            Text commodityName = newCommodity.transform.GetChild(1).GetComponent<Text>();
            Text commodityPrice = newCommodity.transform.GetChild(2).GetComponent<Text>();

            newCommodity.transform.parent = _commoditysTransform;
            commodityImage.sprite = _dataSo.BiologySos[i].BiologySprite;
            commodityName.text = _dataSo.BiologySos[i].BiologyName;
            commodityPrice.text = _dataSo.BiologySos[i].Price.ToString() + "块";
            now = i + 3 * _currentPage;
        }
    }
    
}