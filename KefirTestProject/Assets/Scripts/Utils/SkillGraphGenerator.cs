using System;
using System.Collections.Generic;
using KefirTestProject.Models;
using KefirTestProject.Views;
using UnityEngine;

namespace KefirTestProject.Utils
{
    public class SkillGraphGenerator : MonoBehaviour
    {
        [Serializable]
        public class SkillMapper
        {
            public SkillView SkillView;
            public List<SkillView> Ancestors;

           public Skill CreateSkill()
            {
                return CreateSkillFromView(SkillView);
            }

            private Skill CreateSkillFromView(SkillView skillView)
            {
                var ancestors = new List<Skill>();
                foreach (var ancestor in Ancestors)
                {
                    ancestors.Add(CreateSkillFromView(ancestor));
                }

                return new Skill(skillView.SkillAsset.SkillPoints, ancestors);
            }
        }

        [SerializeField] private List<SkillMapper> _skillMapping;

        [SerializeField] private LineBetweenObjects _connectionLinePrefab;

        private void Awake()
        {
            DrawConnections();
            //GenerateSkillGraph();
        }

        private void DrawConnections()
        {
            foreach (var mappedSkill in _skillMapping)
            {
                foreach (var ancestor in mappedSkill.Ancestors)
                {
                    var connectionInstance = Instantiate(_connectionLinePrefab,
                        mappedSkill.SkillView.transform.parent);

                    connectionInstance.Draw(mappedSkill.SkillView.gameObject, ancestor.gameObject);

                    connectionInstance.transform.SetAsFirstSibling();
                }
            }
        }

        private void GenerateSkillGraph()
        {
            var skillGraph = new SkillGraph();

            foreach (var mappedSkill in _skillMapping)
            {
                skillGraph.AddSkill(mappedSkill.CreateSkill());
            }
        }
    }
}
