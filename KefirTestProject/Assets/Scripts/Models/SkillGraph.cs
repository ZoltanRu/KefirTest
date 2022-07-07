using System.Collections.Generic;

namespace KefirTestProject.Models
{
    public class SkillGraph
    {
        private List<Skill> _skills = new();

        public void AddSkill(Skill skill)
        {
            _skills.Add(skill);
        }
    }
}
