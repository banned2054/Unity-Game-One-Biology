using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class TimeMgr : MonoBehaviour
    {
        private float _dayPerTime;
        private float _timeGoes;
        private bool  _beginFlow;

        [SerializeField]
        private TMP_Text moneyText;

        [SerializeField]
        private TMP_Text techPointText;

        [SerializeField]
        private TMP_Text populationText;

        [SerializeField]
        private TMP_Text timeText;

        [SerializeField]
        private DataSo dataSo;

        [SerializeField]
        private GameObject treePanel;

        [SerializeField]
        private GameObject titlePanel;

        private Transform _treeIcons;

        public List<UpdateBase> updateList;

        private void Start()
        {
            _beginFlow = false;
        }

        public void Init()
        {
            _dayPerTime = dataSo.TimeSpeed;
            _treeIcons  = treePanel.transform.GetChild(2).transform;
            _timeGoes   = 0;
            _beginFlow  = true;
            UpdateUI();
        }

        private void Update()
        {
            if (!_beginFlow) return;
            _timeGoes += Time.deltaTime;
            if (!(_timeGoes >= _dayPerTime)) return;
            _timeGoes = 0;
            UpdateDate();
        }

        private void UpdateDate()
        {
            _timeGoes = 0;
            dataSo.Day++;
            if (dataSo.Day > 12)
            {
                dataSo.Day = 1;
                dataSo.Month++;
            }

            //计算科技点数
            for (var i = dataSo.TechnologyLevel.Count - 1; i >= 0; i--)
            {
                if (!(dataSo.TechnologyPoint >= dataSo.TechnologyLevel[i])) continue;
                dataSo.CurrentLevel = i;
                break;
            }

            foreach (var update in updateList)
            {
                update.UpdateTime();
            }


            UpdateUI();
        }

        public void UpdateUI()
        {
            timeText.text       = $"{dataSo.Month}月{dataSo.Day}日";
            techPointText.text  = dataSo.TechnologyPoint.ToString(CultureInfo.InvariantCulture);
            moneyText.text      = $"{dataSo.Money}块";
            populationText.text = dataSo.Population.ToString(CultureInfo.InvariantCulture);

            UpdateTree();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            _beginFlow     = false;
        }

        public void ContinueGame()
        {
            Time.timeScale = 1f;
            _beginFlow     = true;
        }

        private void UpdateTree()
        {
            for (var i = 0; i <= dataSo.CurrentLevel; i++)
            {
                _treeIcons.GetChild(i).GetComponent<Image>().sprite = dataSo.TreeIcons[i];
            }

            for (var i = dataSo.CurrentLevel + 1; i < 9; i++)
            {
                _treeIcons.GetChild(i).GetComponent<Image>().sprite = dataSo.TreeLockedIcons[i];
            }
        }
    }
}
