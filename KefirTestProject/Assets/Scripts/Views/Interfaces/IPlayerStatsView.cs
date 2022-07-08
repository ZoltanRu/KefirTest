using System;

namespace KefirTestProject.Views.Interfaces
{
    public interface IPlayerStatsView : IView
    {
        event Action<int> SkillPointsChanged;

        void UpdateSkillPointsText(int skillPoints);
    }
}
