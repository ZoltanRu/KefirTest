using System;
using KefirTestProject.Enums;
using KefirTestProject.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTestProject.Views
{
    public class SkillView : MonoBehaviour, ISkillView
    {
        public event Action<int> Selected;

        [SerializeField] private SkillAsset _skillAsset;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _skillPoints;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Button _selectButton;

        public SkillAsset SkillAsset => _skillAsset;

        private void Awake()
        {
            _selectButton.onClick.AddListener(SelectSkill);
        }

        private void Start()
        {
            _name.text = _skillAsset.Name;
            _skillPoints.text = _skillAsset.SkillPoints.ToString();

            if (_skillAsset.SkillIcon == null)
            {
                _skillIcon.gameObject.SetActive(false);
            }
            else
            {
                _skillIcon.sprite = _skillAsset.SkillIcon;
            }
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(SelectSkill);
        }

        private void SelectSkill()
        {
            Selected?.Invoke(SkillAsset.Id);
        }

        public void UpdateSkillStatus(SkillStatus skillStatus)
        {
            switch (skillStatus)
            {
                case SkillStatus.Closed:
                    _skillIcon.color = Color.red;
                    break;
                case SkillStatus.Learned:
                    _skillIcon.color = Color.green;
                    break;
            }
        }
    }
}
