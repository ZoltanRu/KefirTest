using System.Collections.Generic;
using UnityEngine;

namespace KefirTestProject.ScriptableObjects
{
    [CreateAssetMenu]
    public class SkillAsset : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private int _skillPoints;
        [SerializeField] private Sprite _skillIcon;
        [SerializeField] private List<int> _ancestors;

        public int Id => _id;
        public string Name => _name;
        public int SkillPoints => _skillPoints;
        public Sprite SkillIcon => _skillIcon;
        public List<int> Ancestors => _ancestors;
    }
}