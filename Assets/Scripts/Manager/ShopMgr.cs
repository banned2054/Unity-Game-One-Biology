using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;


public class ShopMgr : MonoBehaviour
{
    [SerializeField] private GameObject _commodityPrefab;
    [SerializeField] private GameObject _biologyPrefab;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Transform _commoditysTransform;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private TimeMgr _timeMgr;
    [SerializeField] private DataSo _dataSo;
    [SerializeField] private GroundGroupSo _groundGroupSo;


    private Transform _groundTransform;
    private int _commodityPage = 0;
    private int _currentPage = 0;
    private int _x, _y;

    public void OpenShop(int x, int y)
    {
        _x = x;
        _y = y;
        GameObject grounds = GameObject.Find("Grounds");
        _groundTransform = grounds.transform.GetChild(x * 10 + y);
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
        _upButton.onClick.RemoveAllListeners();
        _downButton.onClick.RemoveAllListeners();
        _upButton.onClick.AddListener(UpPage);
        _downButton.onClick.AddListener(DownPage);
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
            now = i + 3 * _currentPage;
            //实例化新的商品
            GameObject newCommodity = Instantiate(_commodityPrefab, _commoditysTransform).gameObject;
            Image commodityImage = newCommodity.transform.GetChild(0).GetComponent<Image>();
            Text commodityName = newCommodity.transform.GetChild(1).GetComponent<Text>();
            Text commodityPrice = newCommodity.transform.GetChild(2).GetComponent<Text>();
            Text commodityNumb = newCommodity.transform.GetChild(3).GetComponent<Text>();
            Button commodityButton = newCommodity.GetComponent<Button>();

            newCommodity.transform.SetParent(_commoditysTransform);
            commodityImage.sprite = _dataSo.BiologySos[i].BiologySprite;
            commodityName.text = _dataSo.BiologySos[i].BiologyName;
            commodityPrice.text = _dataSo.BiologySos[i].Price.ToString() + "块";
            commodityNumb.text = now.ToString();
            Debug.Log("now = "+now.ToString());
            commodityButton.onClick.AddListener(()=>PutBiology(now));
            Debug.Log("added now = "+now.ToString());
        }
    }

    public void PutBiology(int currentNumb)
    {
        if (_dataSo.BiologySos[currentNumb].Price > _dataSo.Money)
        {
            return;
        }

        _dataSo.Money -= _dataSo.BiologySos[currentNumb].Price;
        

        _shopPanel.SetActive(false);

        Debug.Log(currentNumb);
        Vector3 parentVector3 = _groundTransform.position;
        GameObject newBiology = Instantiate(_biologyPrefab, _groundTransform);
        newBiology.transform.Rotate(-30, 0, 0, Space.Self);
        newBiology.transform.position = new Vector3(parentVector3.x, parentVector3.y + 3, -2);
        newBiology.GetComponent<SpriteRenderer>().sprite = _dataSo.BiologySos[currentNumb].BiologySprite;
        newBiology.transform.localScale = Vector3.one * _dataSo.BiologySos[currentNumb].Size / 100;
        BiologyUpdate biologyUpdate = newBiology.AddComponent<BiologyUpdate>();
        biologyUpdate.Init(_groundGroupSo, _dataSo.BiologySos[currentNumb], _x, _y);
        _timeMgr.UpdateUI();
    }
}