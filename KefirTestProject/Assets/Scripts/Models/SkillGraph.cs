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

            SetupChildren();

            Skills[0].Status = SkillStatus.Learned;

            UpdateSkillStatuses(Skills);
        }

        private void SetupChildren()
        {
            foreach (var skill in Skills)
            {
                foreach (var ancestorId in skill.Ancestors)
                {
                    var ancestor = Skills.First(x => x.Id == ancestorId);
                    ancestor.AddChild(skill.Id);
                }
            }
        }

        private void UpdateSkillStatuses(IEnumerable<Skill> skills)
        {
            foreach (var skill in skills)
            {
                if (skill.Status == SkillStatus.Learned)
                {
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

        public void UpdateSkillStatuses(int id, bool searchDown)
        {
            var skillsToUpdate = new HashSet<Skill>();
            var skill = Skills.First(x => x.Id == id);

            foreach (var childId in skill.Children)
            {
                skillsToUpdate.Add(Skills.First(x => x.Id == childId));
            }

            if (!searchDown)
            {
                foreach (var ancestorId in skill.Ancestors)
                {
                    skillsToUpdate.Add(Skills.First(x => x.Id == ancestorId));
                }
            }

            UpdateSkillStatuses(skillsToUpdate);
        }

        public bool HasConnectionWithRoot(int id)
        {
            var skill = Skills.First(x => x.Id == id);
            if (skill.Status != SkillStatus.Learned)
            {
                return false;
            }

            if (id == 0)
            {
                return true;
            }

            var foundConnection = false;
            foreach (var ancestorId in skill.Ancestors)
            {
                if (HasConnectionWithRoot(ancestorId))
                {
                    foundConnection = true;
                    break;
                }
            }

            return foundConnection;
        }

        //public bool CheckForgetPossibility(int id)
        //{
        //    var foundConnections = _connections.Where(x => x.HasId(id));

        //}
    }
}
