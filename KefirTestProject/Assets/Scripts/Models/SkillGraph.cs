using System.Collections.Generic;
using System.Linq;
using KefirTestProject.Enums;

namespace KefirTestProject.Models
{
    public class SkillGraph
    {
        public IList<Skill> Skills { get; }

        public IList<Skill> LearnedSkills =>
            Skills.Where(x => x.Id != 0 && x.Status == SkillStatus.Learned).ToList();

        public SkillGraph(IList<Skill> skills)
        {
            Skills = skills;

            SetupChildren();

            Skills[0].Status = SkillStatus.Learned;

            UpdateSkillStatusesInternal(Skills);
        }

        public bool CheckForgetPossibility(int id)
        {
            var skill = GetSkillById(id);

            if (skill.Status != SkillStatus.Learned)
            {
                return false;
            }

            foreach (var childId in skill.Children)
            {
                var child = GetSkillById(childId);

                if (child.Status == SkillStatus.Learned)
                {
                    if (!HasConnectionWithRoot(childId, id))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public Skill GetSkillById(int id)
        {
            return Skills.First(x => x.Id == id);
        }

        public void ForgetAll()
        {
            foreach (var learnedSkill in LearnedSkills)
            {
                learnedSkill.Status = SkillStatus.Closed;
            }
            UpdateSkillStatusesInternal(Skills);
        }

        public void UpdateSkillStatuses(int id, SkillOperation operation)
        {
            var skillsToUpdate = new HashSet<Skill>();
            var skill = GetSkillById(id);

            foreach (var childId in skill.Children)
            {
                skillsToUpdate.Add(GetSkillById(childId));
            }

            if (operation == SkillOperation.Forget)
            {
                skillsToUpdate.Add(skill);

                foreach (var ancestorId in skill.Ancestors)
                {
                    skillsToUpdate.Add(GetSkillById(ancestorId));
                }
            }

            UpdateSkillStatusesInternal(skillsToUpdate);
        }

        private bool HasConnectionWithRoot(int id, int? exceptionAncestorId = null)
        {
            var skill = GetSkillById(id);
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
                if (exceptionAncestorId.HasValue && ancestorId == exceptionAncestorId)
                {
                    continue;
                }

                if (HasConnectionWithRoot(ancestorId))
                {
                    foundConnection = true;
                    break;
                }
            }

            return foundConnection;
        }

        private void SetupChildren()
        {
            foreach (var skill in Skills)
            {
                foreach (var ancestorId in skill.Ancestors)
                {
                    var ancestor = GetSkillById(ancestorId);
                    ancestor.AddChild(skill.Id);
                }
            }
        }

        private void UpdateSkillStatusesInternal(IEnumerable<Skill> skills)
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
                    var ancestorSkill = GetSkillById(ancestorId);
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
