using System;
using System.Collections.Generic;
using KefirTestProject.Enums;

namespace KefirTestProject.Models
{
    public class Skill
    {
        public event Action SkillStatusChanged;

        private SkillStatus _status;

        public int Id { get; set; } 
        public int SkillPoints { get; set; }
        public IList<int> Ancestors { get; }
        public IList<int> Children { get; }

        public SkillStatus Status
        {
            get => _status;
            set
            {
                if (_status == value)
                    return;

                _status = value;
                SkillStatusChanged?.Invoke();
            }
        }

        public Skill(int id, int skillPoints, IList<int> ancestors)
        {
            Id = id;
            SkillPoints = skillPoints;
            Ancestors = ancestors;
            Status = SkillStatus.Closed;
            Children = new List<int>();
        }

        public void AddChild(int childId)
        {
            Children.Add(childId);
        }
    }
}
