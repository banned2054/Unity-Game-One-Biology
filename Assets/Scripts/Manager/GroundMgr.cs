using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GroundMgr : MonoBehaviour
{
    private GroundGroupSo _groundGroupSo;
    private GameObject _parentGround; //Ground都是parent的子物体
    private Transform _parentTransform; //保存parent的transform方便操作
    private GameObject _electedGround; //鼠标指向的ground
    private int _electX, _electY;

    private GameObject _infoPanel; //ground信息
    private TimeMgr _timeMgr; //游戏暂停
    private GameObject _maskObejct; //遮罩在ground上

    private Vector3 _defaultMaskPostion = new Vector3(-114514, -114514, 0);

    public void Init(GroundGroupSo groundGroupSo, DataSo dataSo, GameObject maskObject, GameObject infoPanel)
    {
        GroundUpdate groundUpdate = transform.AddComponent<GroundUpdate>();
        groundUpdate.Init(groundGroupSo,dataSo);

        _groundGroupSo = groundGroupSo;
        _parentGround = GameObject.Find("Grounds");
        _parentTransform = _parentGround.transform;
        _infoPanel = infoPanel;

        _timeMgr = GameObject.Find("Events").GetComponent<TimeMgr>();

        _maskObejct = maskObject;

        int x = -5, y = 0;

        List<float> groundLevel = new List<float>(dataSo.GroundLevel);
        List<Material> materials = new List<Material>(dataSo.Materials);

        GameObject groundPrefab = dataSo.GroundPrefab;

        for (int i = 0; i < groundGroupSo.Grounds.Count; i++)
        {
            GroundSo nowGroundSo = groundGroupSo.Grounds[i];
            int j = 0;
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
                y = 0;
            }
            else
            {
                y += 5;
            }

            GameObject newGround = Instantiate(groundPrefab, new Vector3(x, y, 0), Quaternion.identity);
            newGround.GetComponent<MeshRenderer>().material = materials[j];
            newGround.transform.SetParent(_parentTransform);
        }
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && IsMouseOverGameWindow) //鼠标没有点到ui
        {
            //判断鼠标在
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            for (int i = 0; i < _parentTransform.childCount; i++)
            {
                GameObject currentGround = _parentTransform.GetChild(i).gameObject;
                Collider currentCollider = currentGround.GetComponent<Collider>();
                if (currentCollider.Raycast(ray, out hit, 100f))
                {
                    _electedGround = currentGround;
                    _electX = i / 10;
                    _electY = i % 10;
                    Vector3 electedPosition = _electedGround.transform.position;
                    _maskObejct.transform.position = new Vector3(electedPosition.x, electedPosition.y, -0.2f);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                _timeMgr.PauseGame();
                _infoPanel.SetActive(true);

                //info panel 定位
                {
                    Vector3 mousePostion = Input.mousePosition;
                    _infoPanel.transform.position = mousePostion + new Vector3(100, 0, 0);


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
                        _infoPanel.transform.position = new Vector3(mousePostion.x - 100,
                            _infoPanel.transform.position.y,
                            _infoPanel.transform.position.z);
                    }

                    if (_infoPanel.transform.position.x < -550)
                    {
                        _infoPanel.transform.position = new Vector3(-550, _infoPanel.transform.position.y,
                            _infoPanel.transform.position.z);
                    }
                }

                Text infoText = _infoPanel.transform.GetChild(1).GetComponent<Text>();
                Button infoButton = _infoPanel.transform.GetChild(2).GetComponent<Button>();
                Text buttonText = infoButton.transform.GetChild(0).GetComponent<Text>();

                {
                    if (_electedGround.transform.childCount == 0)
                    {
                        infoText.text = "当前没有生物在该土地上";
                        buttonText.text = "打开商店";
                        infoButton.onClick.RemoveAllListeners();
                        infoButton.onClick.AddListener(OpenShop);
                        infoButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        GroundSo currentGroundSo = _groundGroupSo.Grounds[_electX * 10 + _electY];
                        BiologySo currentBiologySo = currentGroundSo.GroundBiologySo;
                        infoText.text = "生物：" + currentBiologySo.BiologyName + "\n";
                        infoText.text += "数量：" + currentGroundSo.BiologyNumb + "\n";
                        infoText.text += "水分：" + currentGroundSo.Water.ToString();
                        infoButton.gameObject.SetActive(false);
                    }
                }
            }
        }

        else
        {
            _maskObejct.transform.position = _defaultMaskPostion;
        }
    }

    bool IsMouseOverGameWindow
    {
        get
        {
            return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x ||
                     Screen.height < Input.mousePosition.y);
        }
    }

    void OpenShop()
    {
        ShopMgr shopMgr = GetComponent<ShopMgr>();
        shopMgr.OpenShop(_electX, _electY);
        _infoPanel.SetActive(false);
    }


}