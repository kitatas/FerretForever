using EFUK;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GimmickController : MonoBehaviour
    {
        private int _counter;
        private int _initCounter;

        private readonly int _interval = 86;

        public void Init()
        {
            _counter = 0;
            _initCounter = 0;
        }

        public void SetUp(GroundView groundView)
        {
            groundView.Activate(true);
            groundView.ActivateChildren(false);

            _counter++;
            if (_counter % _interval == 0)
            {
                _counter = 0;
                // TODO: 橋の看板表示
                return;
            }

            // 橋を作るための空き
            if ((_counter % _interval).IsBetween(1, 3))
            {
                // 開始直後は空きを作らない
                if (++_initCounter <= 3)
                {
                    return;
                }

                groundView.Activate(false);
                return;
            }

            // 橋の前後にはギミックなし
            if ((_counter % _interval).IsBetween(-3, 4))
            {
                return;
            }

            // TODO: ギミック生成
        }
    }
}