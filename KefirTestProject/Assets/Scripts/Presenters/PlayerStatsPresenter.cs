using KefirTestProject.Models;
using KefirTestProject.Views.Interfaces;
using UnityEngine;

namespace KefirTestProject.Presenters
{
    public class PlayerStatsPresenter : MonoBehaviour
    {
        private PlayerStats _playerStats;
        private IPlayerStatsView _playerStatsView;

        private void Awake()
        {
            _playerStats = new PlayerStats();
            _playerStats.SkillPointsChanged += UpdateSkillPointView;

            _playerStatsView = GetComponent<IPlayerStatsView>();
            _playerStatsView.SkillPointAdded += UpdateSkillPointModel;

            UpdateSkillPointView();
        }

        private void OnDestroy()
        {
            _playerStats.SkillPointsChanged += UpdateSkillPointView;
            _playerStatsView.SkillPointAdded -= UpdateSkillPointModel;
        }

        private void UpdateSkillPointView()
        {
            _playerStatsView.UpdateSkillPointsText(_playerStats.SkillPoints);
        }

        private void UpdateSkillPointModel()
        {
            _playerStats.SkillPoints++;
        }
    }
}
