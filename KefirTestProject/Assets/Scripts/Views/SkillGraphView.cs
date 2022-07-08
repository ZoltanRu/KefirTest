using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private LineBetweenObjects _connectionLinePrefab;

        [Header("Skill Selection")] 
        [SerializeField] private Button _learnSkillButton;
        [SerializeField] private Button _forgetSkillButton;

        public IList<SkillView> SkillViews => _skillViews;

        private void Awake()
        {
            _learnSkillButton.onClick.AddListener(LearnSkill);
            _forgetSkillButton.onClick.AddListener(ForgetSkill);

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

        private void ForgetSkill()
        {
            throw new NotImplementedException();
        }

        private void LearnSkill()
        {
            throw new NotImplementedException();
        }

        private void OnSkillSelected(int id)
        {
            SkillSelectionChanged?.Invoke(id);

            //TODO: spawn selector
        }
    }
}
