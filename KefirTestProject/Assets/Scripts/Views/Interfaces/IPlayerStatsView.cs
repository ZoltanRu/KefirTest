using System;

namespace KefirTestProject.Views.Interfaces
{
    public interface IPlayerStatsView : IView
    {
        event Action SkillPointAdded;

        void UpdateSkillPointsText(int skillPoints);
    }
}
