using System;
using KefirTestProject.Enums;
using KefirTestProject.ScriptableObjects;

namespace KefirTestProject.Views
{
    public interface ISkillView : IView
    {
        event Action<int> Selected;

        SkillAsset SkillAsset { get; }

        void UpdateSkillStatus(SkillStatus skillStatus);
    }
}
