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
        private readonly IList<SkillPresenter> _skillPresenters = new List<SkillPresenter>();
        private ISkillGraphView _skillGraphView;
        private SkillGraph _skillGraph;

        private Skill _selectedSkill;

        private void Awake()
        {
            _skillGraphView = GetComponent<ISkillGraphView>();

            _skillGraphView.SkillSelectionChanged += OnSkillSelectionChanged;
            _skillGraphView.LearnSkillClicked += OnLearnSkillClicked;
            _skillGraphView.ForgetSkillClicked += OnForgetSkillClicked;

            foreach (var skillView in _skillGraphView.SkillViews)
            {
                _skillPresenters.Add(skillView.gameObject.GetComponent<SkillPresenter>());
            }

            _skillGraph = new SkillGraph(_skillPresenters
                .Select(x => x.Skill).ToList());
        }

        private void OnDestroy()
        {
            _skillGraphView.SkillSelectionChanged -= OnSkillSelectionChanged;
            _skillGraphView.LearnSkillClicked -= OnLearnSkillClicked;
            _skillGraphView.ForgetSkillClicked -= OnForgetSkillClicked;
        }

        private void OnForgetSkillClicked(int id)
        {
            _selectedSkill.Status = SkillStatus.Opened;
            _skillGraph.UpdateSkillStatuses(id, false);
            _skillGraphView.UpdateSkillInteraction(_selectedSkill.Status, _skillGraph.HasConnectionWithRoot(id));
        }

        private void OnLearnSkillClicked(int id)
        {
            _selectedSkill.Status = SkillStatus.Learned;
            _skillGraph.UpdateSkillStatuses(id, true);
            _skillGraphView.UpdateSkillInteraction(_selectedSkill.Status, _skillGraph.HasConnectionWithRoot(id));
        }

        private void OnSkillSelectionChanged(int id)
        {
            _selectedSkill = _skillGraph.Skills.First(x => x.Id == id);
            _skillGraphView.UpdateSkillInteraction(_selectedSkill.Status, _skillGraph.HasConnectionWithRoot(id));
        }
    }
}
