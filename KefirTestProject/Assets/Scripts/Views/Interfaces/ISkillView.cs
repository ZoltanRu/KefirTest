using KefirTestProject.Enums;
using KefirTestProject.ScriptableObjects;

namespace KefirTestProject.Views
{
    public interface ISkillView : IView
    {
        SkillAsset SkillAsset { get; }

        void UpdateSkillStatus(SkillStatus skillStatus);
    }
}
