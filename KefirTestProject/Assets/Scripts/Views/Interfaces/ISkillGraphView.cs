using System;
using System.Collections.Generic;
using KefirTestProject.Enums;

namespace KefirTestProject.Views.Interfaces
{
    public interface ISkillGraphView : IView
    {
        event Action<int> LearnSkillClicked;
        event Action<int> ForgetSkillClicked;
        event Action<int> SkillSelectionChanged;

        IList<SkillView> SkillViews { get; }

        void UpdateSkillInteraction(SkillStatus skillStatus, 
            bool isForgetPossible, bool isEnoughSkillPoints);
    }
}
