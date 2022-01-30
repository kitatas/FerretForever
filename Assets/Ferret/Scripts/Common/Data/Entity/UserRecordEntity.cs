using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.Common.Data.Entity
{
    public sealed class UserRecordEntity : BaseEntity<UserRecord>
    {
        public bool IsSync()
        {
            return string.IsNullOrEmpty(Get().userName) == false;
        }

        public void UpdateName(string userName)
        {
            var updateRecord = new UserRecord
            {
                userName = userName,
                playCount = Get().playCount,
                currentRecord = new RecordData
                {
                    score = Get().currentRecord.score,
                    victimCount = Get().currentRecord.victimCount,
                },
                highRecord = new RecordData
                {
                    score = Get().highRecord.score,
                    victimCount = Get().highRecord.victimCount,
                },
                totalRecord = new RecordData
                {
                    score = Get().totalRecord.score,
                    victimCount = Get().totalRecord.victimCount,
                },
            };

            Set(updateRecord);
        }

        public void UpdateScore(float score, int victimCount)
        {
            var updateRecord = new UserRecord
            {
                userName = Get().userName,
                playCount = Get().playCount + 1,
                currentRecord = new RecordData
                {
                    score = score,
                    victimCount = victimCount,
                },
                highRecord = new RecordData
                {
                    score = Mathf.Max(Get().highRecord.score, score),
                    victimCount = Mathf.Max(Get().highRecord.victimCount, victimCount),
                },
                totalRecord = new RecordData
                {
                    score = Mathf.Clamp(Get().totalRecord.score + score, 0.0f, float.MaxValue),
                    victimCount = Mathf.Clamp(Get().totalRecord.victimCount + victimCount, 0, int.MaxValue),
                },
            };

            Set(updateRecord);
        }
    }
}