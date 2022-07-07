using KefirTestProject.Views.Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTestProject.Views
{
    public class PlayerStatsView : MonoBehaviour, IPlayerStatsView
    {
        public event Action SkillPointAdded;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text _skillPoints;

        [SerializeField] private Button _addSkillPointButton;

        public void UpdateSkillPointsText(int skillPoints)
        {
            _skillPoints.text = skillPoints.ToString();
        }

        private void Start()
        {
            _addSkillPointButton.onClick.AddListener(AddSkillPoint);
        }

        private void OnDestroy()
        {
            _addSkillPointButton.onClick.RemoveListener(AddSkillPoint);
        }

        private void AddSkillPoint()
        {
            SkillPointAdded?.Invoke();
        }
    }
}
