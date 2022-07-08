using KefirTestProject.Models;
using KefirTestProject.Views;
using UnityEngine;

namespace KefirTestProject.Presenters
{
    public class SkillPresenter : MonoBehaviour
    {
        private ISkillView _skillView;

        public Skill Skill { get; private set; }

        private void Awake()
        {
            _skillView = GetComponent<ISkillView>();

            CreateSkillFromSkillView();

            Skill.SkillStatusChanged += UpdateSkillView;
        }

        private void OnDestroy()
        {
            Skill.SkillStatusChanged -= UpdateSkillView;
        }

        public void CreateSkillFromSkillView()
        {
            var skillAsset = _skillView.SkillAsset;
            Skill = new Skill(skillAsset.Id, skillAsset.SkillPoints, skillAsset.Ancestors);
        }

        private void UpdateSkillView()
        {
            _skillView.UpdateSkillStatus(Skill.Status);
        }
    }
}
