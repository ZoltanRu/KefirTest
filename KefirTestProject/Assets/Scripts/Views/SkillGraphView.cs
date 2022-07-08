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
        public event Action ForgetAllClicked;
        public event Action<int> LearnSkillClicked;
        public event Action<int> ForgetSkillClicked;
        public event Action<int> SkillSelectionChanged;

        [SerializeField] private List<SkillView> _skillViews;

        [Header("Prefabs")]
        [SerializeField] private LineBetweenObjects _connectionLinePrefab;
        [SerializeField] private GameObject _selectorPrefab;

        [Header("Skill Selection")] 
        [SerializeField] private Button _learnSkillButton;
        [SerializeField] private Button _forgetSkillButton;
        [SerializeField] private Button _forgetAllButton;

        private SkillView _selectedView;
        private GameObject _selector;

        public IList<SkillView> SkillViews => _skillViews;

        public void UpdateSkillInteraction(SkillStatus skillStatus,
            bool isForgetPossible,
            bool isForgetAllPossible,
            bool isEnoughSkillPoints)
        {
            _forgetSkillButton.interactable =
                isForgetPossible && _selectedView.SkillAsset.Id != 0;
            _forgetAllButton.interactable = isForgetAllPossible;
            _learnSkillButton.interactable = 
                skillStatus == SkillStatus.Opened && isEnoughSkillPoints;
        }

        private void Awake()
        {
            _learnSkillButton.onClick.AddListener(LearnSkill);
            _forgetSkillButton.onClick.AddListener(ForgetSkill);
            _forgetAllButton.onClick.AddListener(ForgetAll);
            _learnSkillButton.interactable = false;
            _forgetSkillButton.interactable = false;
            _forgetAllButton.interactable = false;

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
            _forgetAllButton.onClick.RemoveListener(ForgetAll);
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

        private void ForgetAll()
        {
            ForgetAllClicked?.Invoke();
        }

        private void ForgetSkill()
        {
            ForgetSkillClicked?.Invoke(_selectedView.SkillAsset.Id);
        }

        private void LearnSkill()
        {
            LearnSkillClicked?.Invoke(_selectedView.SkillAsset.Id);
        }
    }
}
