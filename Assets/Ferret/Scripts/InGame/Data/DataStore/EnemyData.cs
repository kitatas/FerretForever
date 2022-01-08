using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EnemyData), menuName = "DataTable/" + nameof(EnemyData), order = 0)]
    public sealed class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyType enemyType = default;
        [SerializeField] private EnemyController enemyController = default;

        public EnemyType type => enemyType;
        public EnemyController enemy => enemyController;
    }
}