using SO;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UpdateObject;

namespace Manager
{
    public class GroundMgr : MonoBehaviour
    {
        private readonly Vector3 _defaultMaskPosition = new(-114514, -114514, 0);

        private GameObject _parentGround;    //Ground都是parent的子物体
        private GameObject _electedGround;   //鼠标指向的ground
        private GameObject _infoPanel;       //ground信息
        private GameObject _maskObject;      //遮罩在ground上
        private Transform  _parentTransform; //保存parent的transform方便操作

        private Camera   _camera;
        private Button   _infoButton;
        private TMP_Text _infoText;
        private TMP_Text _buttonText;

        private GroundGroupSo _groundGroupSo;
        private TimeMgr       _timeMgr; //游戏暂停

        private int _electX, _electY;

        public void Init(GroundGroupSo groundGroupSo, DataSo dataSo, GameObject maskObject, GameObject infoPanel)
        {
            var groundUpdate = transform.AddComponent<GroundUpdate>();
            groundUpdate.Init(groundGroupSo, dataSo);

            _groundGroupSo   = groundGroupSo;
            _parentGround    = GameObject.Find("Grounds");
            _parentTransform = _parentGround.transform;
            _infoPanel       = infoPanel;

            _timeMgr = GameObject.Find("Events").GetComponent<TimeMgr>();

            _maskObject = maskObject;

            int x = -5, y = 0;

            var groundLevel = new List<float>(dataSo.GroundLevel);
            var materials   = new List<Material>(dataSo.Materials);

            var groundPrefab = dataSo.GroundPrefab;

            for (var i = 0; i < groundGroupSo.grounds.Count; i++)
            {
                var nowGroundSo = groundGroupSo.grounds[i];
                int j;
                for (j = groundLevel.Count - 1; j >= 0; j--)
                {
                    if (nowGroundSo.Water >= groundLevel[j])
                    {
                        break;
                    }
                }

                if (i % 10 == 0)
                {
                    x += 5;
                    y =  0;
                }
                else
                {
                    y += 5;
                }

                var newGround = Instantiate(groundPrefab, new Vector3(x, y, 0), Quaternion.identity);
                newGround.GetComponent<MeshRenderer>().material = materials[j];
                newGround.transform.SetParent(_parentTransform);
            }

            _camera     = GameObject.Find("Main Camera").GetComponent<Camera>();
            _infoButton = _infoPanel.transform.GetChild(2).GetComponent<Button>();
            _infoText   = _infoPanel.transform.GetChild(1).GetComponent<TMP_Text>();
            _buttonText = _infoButton.transform.GetChild(0).GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && IsMouseOverGameWindow) //鼠标没有点到ui
            {
                //判断鼠标在
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                for (var i = 0; i < _parentTransform.childCount; i++)
                {
                    var currentGround   = _parentTransform.GetChild(i).gameObject;
                    var currentCollider = currentGround.GetComponent<Collider>();
                    if (!currentCollider.Raycast(ray, out _, 100f)) continue;
                    _electedGround = currentGround;
                    _electX        = i / 10;
                    _electY        = i % 10;
                    var electedPosition = _electedGround.transform.position;
                    _maskObject.transform.position = new Vector3(electedPosition.x, electedPosition.y, -0.2f);
                    break;
                }

                if (!Input.GetMouseButtonDown(0)) return;
                _timeMgr.PauseGame();
                _infoPanel.SetActive(true);

                //info panel 定位
                {
                    var mousePosition = Input.mousePosition;
                    _infoPanel.transform.position = mousePosition + new Vector3(100, 0, 0);


                    if (_infoPanel.transform.position.y > 547)
                    {
                        _infoPanel.transform.position = new Vector3(_infoPanel.transform.position.x, 547,
                                                                    _infoPanel.transform.position.z);
                    }

                    if (_infoPanel.transform.position.y < 90)
                    {
                        _infoPanel.transform.position = new Vector3(_infoPanel.transform.position.x, 90,
                                                                    _infoPanel.transform.position.z);
                    }

                    if (_infoPanel.transform.position.x > 540)
                    {
                        _infoPanel.transform.position = new Vector3(mousePosition.x - 100,
                                                                    _infoPanel.transform.position.y,
                                                                    _infoPanel.transform.position.z);
                    }

                    if (_infoPanel.transform.position.x < -550)
                    {
                        _infoPanel.transform.position = new Vector3(-550, _infoPanel.transform.position.y,
                                                                    _infoPanel.transform.position.z);
                    }
                }

                {
                    if (_electedGround.transform.childCount == 0)
                    {
                        _infoText.text   = "当前没有生物在该土地上";
                        _buttonText.text = "打开商店";
                        _infoButton.onClick.RemoveAllListeners();
                        _infoButton.onClick.AddListener(OpenShop);
                        _infoButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        var currentGroundSo  = _groundGroupSo.grounds[_electX * 10 + _electY];
                        var currentBiologySo = currentGroundSo.GroundBiologySo;
                        _infoText.text =  $"生物：{currentBiologySo.biologyName}\n";
                        _infoText.text += $"数量：{currentGroundSo.BiologyNumb}\n";
                        _infoText.text += $"水分：{currentGroundSo.Water}";
                        _infoButton.gameObject.SetActive(false);
                    }
                }
            }

            else
            {
                _maskObject.transform.position = _defaultMaskPosition;
            }
        }

        private static bool IsMouseOverGameWindow =>
            !(0             > Input.mousePosition.x ||
              0             > Input.mousePosition.y ||
              Screen.width  < Input.mousePosition.x ||
              Screen.height < Input.mousePosition.y);

        private void OpenShop()
        {
            var shopMgr = GetComponent<ShopMgr>();
            shopMgr.OpenShop(_electX, _electY);
            _infoPanel.SetActive(false);
        }
    }
}
