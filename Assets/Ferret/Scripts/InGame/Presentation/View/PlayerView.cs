using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private Collider2D collider2d = default;
        [SerializeField] private SpriteRenderer sprite = default;

        public void Init()
        {
            
        }

        public void SetUp()
        {
            collider2d.enabled = true;
            sprite.sortingLayerName = SortingLayerConfig.CHARA;
        }

        public void SetUpBridge()
        {
            collider2d.enabled = false;
            sprite.sortingLayerName = SortingLayerConfig.VICTIM;
        }
    }
}