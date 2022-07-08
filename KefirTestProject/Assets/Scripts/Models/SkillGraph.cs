using System.Collections.Generic;
using System.Linq;
using KefirTestProject.Enums;

namespace KefirTestProject.Models
{
    public class SkillGraph
    {
        private IList<Skill> _skills;

        public SkillGraph(IList<Skill> skills)
        {
            _skills = skills;

            SetSkillStatuses();
        }

        private void SetSkillStatuses()
        {
            foreach (var skill in _skills)
            {
                if (skill.Id == 0)
                {
                    skill.Status = SkillStatus.Learned;
                    continue;
                }

                skill.Status = SkillStatus.Opened;

                foreach (var ancestorId in skill.Ancestors)
                {
                    var ancestorSkill = _skills.First(x => x.Id == ancestorId);
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
