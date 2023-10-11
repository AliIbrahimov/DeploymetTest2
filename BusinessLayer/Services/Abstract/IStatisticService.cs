using CoreLayer.Utilities.Results;
using EntityLayer.DTOs.Statistic;
using EntityLayer.Models;

namespace BusinessLayer.Services.Abstract;

public interface IStatisticService
{
    Task<IDataResult<Statistic>> GetStatistic(int id);
    Task<IResult> UpdateStatistic(int id, PostStatisticDto StatisticDto);

}
