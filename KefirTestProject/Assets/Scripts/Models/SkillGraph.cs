using System.Collections.Generic;
using System.Linq;
using KefirTestProject.Enums;

namespace KefirTestProject.Models
{
    public class SkillGraph
    {
        public IList<Skill> Skills { get; }

        public SkillGraph(IList<Skill> skills)
        {
            Skills = skills;

            SetSkillStatuses();
        }

        private void SetSkillStatuses()
        {
            foreach (var skill in Skills)
            {
                if (skill.Id == 0)
                {
                    skill.Status = SkillStatus.Learned;
                    continue;
                }

                skill.Status = SkillStatus.Opened;

                foreach (var ancestorId in skill.Ancestors)
                {
                    var ancestorSkill = Skills.First(x => x.Id == ancestorId);
                    if (ancestorSkill.Status != SkillStatus.Learned)
                    {
                        skill.Status = SkillStatus.Closed;
                        break;
                    }
                }
            }
        }
    }
}
