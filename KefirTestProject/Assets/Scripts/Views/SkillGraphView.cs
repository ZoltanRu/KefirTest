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

        [SerializeField] private List<SkillView> _skillViews;

        [Header("Prefabs")]
        [SerializeField] private LineBetweenObjects _connectionLinePrefab;
        [SerializeField] private GameObject _selectorPrefab;

        [Header("Skill Selection")] 
        [SerializeField] private Button _learnSkillButton;
        [SerializeField] private Button _forgetSkillButton;

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
            SkillSelectionChanged?.Invoke(id);

            if (_selector == null)
            {
                _selector = Instantiate(_selectorPrefab, transform);
                _selector.transform.SetAsFirstSibling();
            }

            var selectedView = _skillViews.First(x => x.SkillAsset.Id == id);
            _selector.transform.localPosition = selectedView.transform.localPosition;
        }

        private void ForgetSkill()
        {
            throw new NotImplementedException();
        }

        private void LearnSkill()
        {
            throw new NotImplementedException();
        }

        public void UpdateSkillInteraction(SkillStatus skillStatus)
        {
            switch (skillStatus)
            {
                case SkillStatus.Closed:
                    _learnSkillButton.interactable = false;
                    _forgetSkillButton.interactable = false;
                    break;
                case SkillStatus.Learned:
                    _learnSkillButton.interactable = false;
                    _forgetSkillButton.interactable = true;
                    break;
                case SkillStatus.Opened:
                    _learnSkillButton.interactable = true;
                    _forgetSkillButton.interactable = false;
                    break;
            }
        }
    }
}
