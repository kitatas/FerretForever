using System;
using System.Collections.Generic;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Data.Entity;
using Ferret.InGame.Domain.Repository;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class AchievementUseCase
    {
        private readonly AchievementMasterEntity _achievementMasterEntity;
        private readonly UserRecordEntity _userRecordEntity;
        private readonly AchievementRankRepository _achievementRankRepository;

        public AchievementUseCase(AchievementMasterEntity achievementMasterEntity, UserRecordEntity userRecordEntity,
            AchievementRankRepository achievementRankRepository)
        {
            _achievementMasterEntity = achievementMasterEntity;
            _userRecordEntity = userRecordEntity;
            _achievementRankRepository = achievementRankRepository;
        }

        public IEnumerable<AchievementData> GetAchievementStatus()
        {
            foreach (var data in _achievementMasterEntity.Get())
            {
                var tableData = _achievementRankRepository.Find(data.rank);
                if (tableData != null)
                {
                    data.color = tableData.color;
                }

                switch (data.type)
                {
                    case AchievementType.HighScore:
                        data.isAchieve = _userRecordEntity.Get().highRecord.score >= data.value;
                        data.detail = $"ハイスコアで{data.value.ToString()}m以上の記録を出した！";
                        break;
                    case AchievementType.TotalScore:
                        data.isAchieve = _userRecordEntity.Get().totalRecord.score >= data.value;
                        data.detail = $"合計で{data.value.ToString()}m以上走った！";
                        break;
                    case AchievementType.TotalVictim:
                        data.isAchieve = _userRecordEntity.Get().totalRecord.victimCount >= data.value;
                        data.detail = $"合計で{data.value.ToString()}匹以上を犠牲にした...";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(data.type), data.type, null);
                }
            }

            return _achievementMasterEntity.Get();
        }
    }
}