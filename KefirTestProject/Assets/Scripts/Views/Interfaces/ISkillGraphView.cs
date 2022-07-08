using System;
using System.Collections.Generic;

namespace KefirTestProject.Views.Interfaces
{
    public interface ISkillGraphView : IView
    {
        event Action<int> SkillSelectionChanged;

        public IList<SkillView> SkillViews { get; }
    }
}
