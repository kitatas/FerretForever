using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PlayerView : MonoBehaviour
    {
        private Collider2D _collider;
        private SpriteRenderer _sprite;

        public void Init()
        {
            _collider = GetComponent<Collider2D>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetUp()
        {
            _collider.enabled = true;
            _sprite.sortingLayerName = SortingLayerConfig.CHARA;
        }

        public void SetUpBridge()
        {
            _collider.enabled = false;
            _sprite.sortingLayerName = SortingLayerConfig.VICTIM;
        }
    }
}