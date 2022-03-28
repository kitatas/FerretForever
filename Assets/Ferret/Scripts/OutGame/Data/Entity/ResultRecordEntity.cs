namespace Ferret.OutGame.Data.Entity
{
    public struct ResultRecordEntity
    {
        public readonly float highScore;
        public readonly float currentScore;
        public readonly bool isNewRecord;

        public ResultRecordEntity(float highScore, float currentScore, bool isNewRecord)
        {
            this.highScore = highScore;
            this.currentScore = currentScore;
            this.isNewRecord = isNewRecord;
        }
    }
}