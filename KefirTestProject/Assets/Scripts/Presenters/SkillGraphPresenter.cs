using System.Collections.Generic;
using System.Linq;
using KefirTestProject.Enums;
using KefirTestProject.Models;
using KefirTestProject.Views.Interfaces;
using UnityEngine;

namespace KefirTestProject.Presenters
{
    public class SkillGraphPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerStatsPresenter _playerStatsPresenter;

        private readonly IList<SkillPresenter> _skillPresenters = new List<SkillPresenter>();
        private ISkillGraphView _skillGraphView;
        private SkillGraph _skillGraph;
        private Skill _selectedSkill;

        private bool IsEnoughSkillPoints => _selectedSkill.SkillPoints <= _playerStatsPresenter.PlayerStats.SkillPoints;

        private void Start()
        {
            _playerStatsPresenter.PlayerStats.SkillPointsChanged += UpdateSkillInteraction;

            _skillGraphView = GetComponent<ISkillGraphView>();
            _skillGraphView.LearnSkillClicked += OnLearnSkillClicked;
            _skillGraphView.ForgetSkillClicked += OnForgetSkillClicked;
            _skillGraphView.ForgetAllClicked += OnForgetAllClicked;
            _skillGraphView.SkillSelectionChanged += OnSkillSelectionChanged;

            foreach (var skillView in _skillGraphView.SkillViews)
            {
                _skillPresenters.Add(skillView.gameObject.GetComponent<SkillPresenter>());
            }

            _skillGraph = new SkillGraph(_skillPresenters
                .Select(x => x.Skill).ToList());
        }

        private void OnDestroy()
        {
            _playerStatsPresenter.PlayerStats.SkillPointsChanged -= UpdateSkillInteraction;

            _skillGraphView.LearnSkillClicked -= OnLearnSkillClicked;
            _skillGraphView.ForgetSkillClicked -= OnForgetSkillClicked;
            _skillGraphView.ForgetAllClicked -= OnForgetAllClicked;
            _skillGraphView.SkillSelectionChanged -= OnSkillSelectionChanged;
        }

        private void OnForgetAllClicked()
        {
            var skillPoints = _skillGraph.LearnedSkills.Sum(x => x.SkillPoints); ;
            _skillGraph.ForgetAll();
            _playerStatsPresenter.PlayerStats.SkillPoints += skillPoints;
        }

        private void OnForgetSkillClicked(int id)
        {
            _selectedSkill.Status = SkillStatus.Opened;
            _skillGraph.UpdateSkillStatuses(id, SkillOperation.Forget);
            _playerStatsPresenter.PlayerStats.SkillPoints += _selectedSkill.SkillPoints;
        }

        private void OnLearnSkillClicked(int id)
        {
            _selectedSkill.Status = SkillStatus.Learned;
            _skillGraph.UpdateSkillStatuses(id, SkillOperation.Learn);
            _playerStatsPresenter.PlayerStats.SkillPoints -= _selectedSkill.SkillPoints;
        }

        private void OnSkillSelectionChanged(int id)
        {
            _selectedSkill = _skillGraph.GetSkillById(id);
            UpdateSkillInteraction();
        }

        private void UpdateSkillInteraction()
        {
            if (_selectedSkill == null)
            {
                return;
            }

            _skillGraphView.UpdateSkillInteraction(_selectedSkill.Status,
                _skillGraph.CheckForgetPossibility(_selectedSkill.Id),
                _skillGraph.LearnedSkills.Count > 0,
                IsEnoughSkillPoints);
        }
    }
}
