using System.Collections.Generic;
using System.Linq;
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

        private int _selectedSkillId = 0;

        private void Awake()
        {
            _skillGraphView = GetComponent<ISkillGraphView>();

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
            foreach (var skillView in _skillGraphView.SkillViews)
            {
                skillView.Selected -= OnSkillSelectionChanged;
            }
        }

        private void OnSkillSelectionChanged(int id)
        {
            _selectedSkillId = id;
        }
    }
}
