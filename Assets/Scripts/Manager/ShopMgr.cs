using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpdateObject;

namespace Manager
{
    public class ShopMgr : MonoBehaviour
    {
        [SerializeField]
        private GameObject commodityPrefab;

        [SerializeField]
        private GameObject biologyPrefab;

        [SerializeField]
        private GameObject shopPanel;

        [SerializeField]
        private Transform commoditiesTransform;

        [SerializeField]
        private Button upButton;

        [SerializeField]
        private Button downButton;

        [SerializeField]
        private TimeMgr timeMgr;

        [SerializeField]
        private DataSo dataSo;

        [SerializeField]
        private GroundGroupSo groundGroupSo;

        [SerializeField]
        private AudioMgr audioMgr;

        private Transform   _groundTransform;
        private ToggleGroup _toggleGroup;

        private int _commodityPage;
        private int _currentPage;
        private int _x, _y;

        public void OpenShop(int x, int y)
        {
            _x           = x;
            _y           = y;
            _toggleGroup = commoditiesTransform.GetComponent<ToggleGroup>();
            var grounds = GameObject.Find("Grounds");
            _groundTransform = grounds.transform.GetChild(x * 10 + y);
            _currentPage     = 0;
            shopPanel.SetActive(true);
            if (dataSo.CurrentLevel > 3)
            {
                _commodityPage = dataSo.CurrentLevel / 3;
            }
            else
            {
                _commodityPage = 0;
            }

            ClearPage();
            upButton.onClick.RemoveAllListeners();
            downButton.onClick.RemoveAllListeners();
            upButton.onClick.AddListener(UpPage);
            downButton.onClick.AddListener(DownPage);
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
            foreach (Transform child in commoditiesTransform)
            {
                Destroy(child.gameObject);
            }

            var i   = 0;
            var now = 3 * _currentPage;
            for (i = 0; i < 3 && now <= dataSo.CurrentLevel; i++)
            {
                now = i + 3 * _currentPage;
                //实例化新的商品
                var newCommodity = Instantiate(commodityPrefab, commoditiesTransform).gameObject;

                var commodityImage  = newCommodity.transform.GetChild(1).GetComponent<Image>();
                var commodityName   = newCommodity.transform.GetChild(2).GetComponent<TMP_Text>();
                var commodityPrice  = newCommodity.transform.GetChild(3).GetComponent<TMP_Text>();
                var commodityNumb   = newCommodity.transform.GetChild(4).GetComponent<TMP_Text>();
                var commodityToggle = newCommodity.GetComponent<Toggle>();

                newCommodity.transform.SetParent(commoditiesTransform);

                commodityToggle.group = _toggleGroup;
                commodityImage.sprite = dataSo.BiologySos[now].biologySprite;
                commodityName.text    = dataSo.BiologySos[now].biologyName;
                commodityPrice.text   = $"{dataSo.BiologySos[now].price}块";
                commodityNumb.text    = now.ToString();
                commodityToggle.onValueChanged.AddListener(delegate { PutBiology(commodityToggle); });
                now++;
            }
        }

        public void PutBiology(Toggle paraToggle)
        {
            if (!paraToggle.isOn) return;
            var currentNumb = 0;
            for (var i = 0; i < commoditiesTransform.childCount; i++)
            {
                var currentToggle = commoditiesTransform.GetChild(i).GetComponent<Toggle>();
                if (!currentToggle.isOn) continue;
                currentNumb = i + 3 * _currentPage;
                break;
            }

            if (dataSo.BiologySos[currentNumb].price > dataSo.Money)
            {
                return;
            }

            dataSo.Money -= dataSo.BiologySos[currentNumb].price;


            shopPanel.SetActive(false);

            var parentVector3 = _groundTransform.position;
            var newBiology    = Instantiate(biologyPrefab, _groundTransform);
            newBiology.transform.Rotate(-30, 0, 0, Space.Self);
            newBiology.transform.position = new Vector3(parentVector3.x, parentVector3.y, -0.5f) +
                                            dataSo.BiologySos[currentNumb].PositionOffset;

            newBiology.GetComponent<SpriteRenderer>().sprite = dataSo.BiologySos[currentNumb].biologySprite;
            newBiology.transform.localScale                  = Vector3.one * dataSo.BiologySos[currentNumb].size / 100;
            var biologyUpdate = newBiology.AddComponent<BiologyUpdate>();
            biologyUpdate.Init(groundGroupSo, dataSo.BiologySos[currentNumb], _x, _y);
            timeMgr.ContinueGame();
            timeMgr.UpdateUI();
            audioMgr.Play_audio();
        }
    }
}
