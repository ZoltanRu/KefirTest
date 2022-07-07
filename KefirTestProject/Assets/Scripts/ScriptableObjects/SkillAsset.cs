using UnityEngine;

namespace KefirTestProject.ScriptableObjects
{
    [CreateAssetMenu]
    public class SkillAsset : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _skillPoints;
        [SerializeField] private Sprite _skillIcon;

        public string Name => _name;
        public int SkillPoints => _skillPoints;
        public Sprite SkillIcon => _skillIcon;
    }
}