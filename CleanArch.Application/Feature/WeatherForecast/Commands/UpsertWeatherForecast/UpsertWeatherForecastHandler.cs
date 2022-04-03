using AutoMapper;
using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastHandler : IRequestHandler<UpsertWeatherForecastCommand, WeatherForecastVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpsertWeatherForecastHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<WeatherForecastVM> Handle(UpsertWeatherForecastCommand command, CancellationToken cancellationToken)
        {
            var weatherForecast = _mapper.Map<WeatherForecastTbl>(command);
            var weather = await _unitOfWork.WeatherForecastRepository.UpsertWeatherForecast(weatherForecast);
            return _mapper.Map<WeatherForecastVM>(weather);
        }
    }
}
