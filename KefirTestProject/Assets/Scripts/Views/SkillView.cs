using KefirTestProject.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTestProject.Views
{
    public class SkillView : MonoBehaviour, ISkillView
    {
        [SerializeField] private SkillAsset _skillAsset;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _skillPoints;
        [SerializeField] private Image _skillIcon;

        public SkillAsset SkillAsset => _skillAsset;

        private void Start()
        {
            _name.text = _skillAsset.Name;
            _skillPoints.text = _skillAsset.SkillPoints.ToString();

            if (_skillAsset.SkillIcon == null)
            {
                _skillIcon.gameObject.SetActive(false);
            }
            else
            {
                _skillIcon.sprite = _skillAsset.SkillIcon;
            }
        }
    }
}
