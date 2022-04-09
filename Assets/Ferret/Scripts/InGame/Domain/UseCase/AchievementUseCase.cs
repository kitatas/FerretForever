using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<AchievementData> GetAchievementStatus()
        {
            foreach (var data in _achievementMasterEntity.Get())
            {
                var tableData = _achievementRankRepository.Find(data.rank);
                if (tableData != null)
                {
                    data.color = tableData.color;
                }

                data.isAchieve = data.type switch
                {
                    AchievementType.PlayCount   => _userRecordEntity.Get().playCount >= data.value,
                    AchievementType.HighScore   => _userRecordEntity.Get().highRecord.score >= data.value,
                    AchievementType.HighVictim  => _userRecordEntity.Get().highRecord.victimCount >= data.value,
                    AchievementType.TotalScore  => _userRecordEntity.Get().totalRecord.score >= data.value,
                    AchievementType.TotalVictim => _userRecordEntity.Get().totalRecord.victimCount >= data.value,
                    _ => throw new ArgumentOutOfRangeException(nameof(data.type), data.type, null)
                };
            }

            return _achievementMasterEntity.Get().ToList();
        }
    }
}