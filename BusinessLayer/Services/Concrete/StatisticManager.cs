using AutoMapper;
using BusinessLayer.Constants;
using BusinessLayer.Services.Abstract;
using CoreLayer.Utilities.Results;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.DTOs.Statistic;
using EntityLayer.Models;

namespace BusinessLayer.Services.Concrete;

public class StatisticManager : IStatisticService
{
    private readonly IStatisticDal _statisticDal;
    private readonly IMapper _mapper;

    public StatisticManager(IStatisticDal statisticDal, IMapper mapper)
    {
        _statisticDal = statisticDal ?? throw new ArgumentNullException(nameof(statisticDal));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IDataResult<Statistic>> GetStatistic(int id)
    {
        try
        {
            if (!(await _statisticDal.IsExistAsync(s=>s.Id==id)))
                return new ErrorDataResult<Statistic>(Messages.NotFound);
            var statistic = await GetNonDeletedStatistic(id);
            return statistic != null
                ? new SuccessDataResult<Statistic>(statistic)
                : new ErrorDataResult<Statistic>("Statistic is null");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<Statistic>(ex.Message);
        }
    }

    public async Task<IResult> UpdateStatistic(int id, PostStatisticDto StatisticDto)
    {
        try
        {
            if (!(await _statisticDal.IsExistAsync(s => s.Id == id)))
                return new ErrorDataResult<Statistic>(Messages.NotFound);

            var existingStatistic = await GetNonDeletedStatistic(id);

            if (existingStatistic == null)
                return new ErrorDataResult<Statistic>("Statistic is null");

            existingStatistic.Customer = StatisticDto.Customer;
            existingStatistic.ProjectsDone = StatisticDto.ProjectsDone;
            existingStatistic.WinAwards = StatisticDto.WinAwards;

            _statisticDal.Update(existingStatistic);

            return new SuccessDataResult<PostStatisticDto>(Messages.Updated);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<Statistic>($"An error occurred: {ex.Message}");
        }
    }
    private async Task<Statistic> GetNonDeletedStatistic(int id)
    {
        return await _statisticDal.GetAsync(p => p.Id == id && !p.isDeleted);
    }

}
