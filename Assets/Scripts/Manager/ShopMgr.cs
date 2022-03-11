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
    [SerializeField] private AudioMgr _audioMgr;


    private Transform _groundTransform;
    private int _commodityPage = 0;
    private int _currentPage = 0;
    private ToggleGroup _toggleGroup;
    private int _x, _y;

    public void OpenShop(int x, int y)
    {
        _x = x;
        _y = y;
        _toggleGroup = _commoditysTransform.GetComponent<ToggleGroup>();
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
        Debug.Log("down begin");
        int i = 0;
        int now = 3 * _currentPage;
        for (i = 0; i < 3 && now <= _dataSo.CurrentLevel; i++)
        {
            now = i + 3 * _currentPage;
            Debug.Log("now " + now);
            //实例化新的商品
            GameObject newCommodity = Instantiate(_commodityPrefab, _commoditysTransform).gameObject;
            Image commodityImage = newCommodity.transform.GetChild(1).GetComponent<Image>();
            Text commodityName = newCommodity.transform.GetChild(2).GetComponent<Text>();
            Text commodityPrice = newCommodity.transform.GetChild(3).GetComponent<Text>();
            Text commodityNumb = newCommodity.transform.GetChild(4).GetComponent<Text>();
            Toggle commodityToggle = newCommodity.GetComponent<Toggle>();

            newCommodity.transform.SetParent(_commoditysTransform);
            commodityToggle.group = _toggleGroup;
            commodityImage.sprite = _dataSo.BiologySos[now].BiologySprite;
            commodityName.text = _dataSo.BiologySos[now].BiologyName;
            commodityPrice.text = _dataSo.BiologySos[now].Price.ToString() + "块";
            commodityNumb.text = now.ToString();
            commodityToggle.onValueChanged.AddListener(delegate { PutBiology(commodityToggle); });
            now++;
        }
    }

    public void PutBiology(Toggle paraToggle)
    {
        if (!paraToggle.isOn) return;
        int currentNumb = 0;
        for (int i = 0; i < _commoditysTransform.childCount; i++)
        {
            Toggle currenToggle = _commoditysTransform.GetChild(i).GetComponent<Toggle>();
            if (currenToggle.isOn)
            {
                currentNumb = i + 3 * _currentPage;
                break;
            }
        }

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
        newBiology.transform.position = new Vector3(parentVector3.x, parentVector3.y, -0.5f);
        newBiology.GetComponent<SpriteRenderer>().sprite = _dataSo.BiologySos[currentNumb].BiologySprite;
        newBiology.transform.localScale = Vector3.one * _dataSo.BiologySos[currentNumb].Size / 100;
        BiologyUpdate biologyUpdate = newBiology.AddComponent<BiologyUpdate>();
        biologyUpdate.Init(_groundGroupSo, _dataSo.BiologySos[currentNumb], _x, _y);
        _timeMgr.ContinueGame();
        _timeMgr.UpdateUI();
        _audioMgr.Play_audio();
    }
}