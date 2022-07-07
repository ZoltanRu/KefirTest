using System.Collections.Generic;

namespace KefirTestProject.Models
{
    public class Skill
    {
        public int SkillPoints { get; set; }

        public List<Skill> Ancestors { get; private set; }

        public Skill(int skillPoints, List<Skill> ancestors = null)
        {
            SkillPoints = skillPoints;
            Ancestors = ancestors;
        }
    }
}
