using System;
using System.Collections.Generic;
using System.Linq;
using KefirTestProject.Enums;
using KefirTestProject.Utils;
using KefirTestProject.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTestProject.Views
{
    public class SkillGraphView : MonoBehaviour, ISkillGraphView
    {
        public event Action<int> SkillSelectionChanged;
        public event Action<int> LearnSkillClicked;
        public event Action<int> ForgetSkillClicked;


        [SerializeField] private List<SkillView> _skillViews;

        [Header("Prefabs")]
        [SerializeField] private LineBetweenObjects _connectionLinePrefab;
        [SerializeField] private GameObject _selectorPrefab;

        [Header("Skill Selection")] 
        [SerializeField] private Button _learnSkillButton;
        [SerializeField] private Button _forgetSkillButton;

        private SkillView _selectedView;
        private GameObject _selector;

        public IList<SkillView> SkillViews => _skillViews;

        private void Awake()
        {
            _learnSkillButton.onClick.AddListener(LearnSkill);
            _forgetSkillButton.onClick.AddListener(ForgetSkill);
            _learnSkillButton.interactable = false;
            _forgetSkillButton.interactable = false;

            SetupViews();
        }

        private void OnDestroy()
        {
            foreach (var skillView in _skillViews)
            {
                skillView.Selected -= OnSkillSelected;
            }

            _learnSkillButton.onClick.RemoveListener(LearnSkill);
            _forgetSkillButton.onClick.RemoveListener(ForgetSkill);
        }

        private void SetupViews()
        {
            foreach (var skillView in _skillViews)
            {
                skillView.Selected += OnSkillSelected;

                foreach (var ancestorId in skillView.SkillAsset.Ancestors)
                {
                    var ancestorView = _skillViews.First(x => x.SkillAsset.Id == ancestorId);
                    var connectionInstance = Instantiate(_connectionLinePrefab,
                        transform);

                    connectionInstance.Draw(skillView.gameObject, ancestorView.gameObject);
                    connectionInstance.transform.SetAsFirstSibling();
                }
            }
        }

        private void OnSkillSelected(int id)
        {
            if (_selector == null)
            {
                _selector = Instantiate(_selectorPrefab, transform);
                _selector.transform.SetAsFirstSibling();
            }

            _selectedView = _skillViews.First(x => x.SkillAsset.Id == id);
            _selector.transform.localPosition = _selectedView.transform.localPosition;

            SkillSelectionChanged?.Invoke(id);
        }

        private void ForgetSkill()
        {
            ForgetSkillClicked?.Invoke(_selectedView.SkillAsset.Id);
        }

        private void LearnSkill()
        {
            LearnSkillClicked?.Invoke(_selectedView.SkillAsset.Id);
        }

        public void UpdateSkillInteraction(SkillStatus skillStatus, bool hasConnectionWithRoot)
        {
            _forgetSkillButton.interactable = 
                hasConnectionWithRoot && _selectedView.SkillAsset.Id != 0;
            _learnSkillButton.interactable = skillStatus == SkillStatus.Opened;
        }
    }
}
