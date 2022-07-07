using System;

namespace KefirTestProject.Models
{
    public class PlayerStats
    {
        public event Action SkillPointsChanged;

        private int _skillPoints;

        public int SkillPoints
        {
            get => _skillPoints;
            set
            {
                if (_skillPoints == value)
                    return;

                _skillPoints = value;
                SkillPointsChanged?.Invoke();
            }
        }
    }
}
