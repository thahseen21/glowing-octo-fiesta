using AutoMapper;
using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastHandler : IRequestHandler<UpsertWeatherForecastCommand, WeatherForecastVM>
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IMapper _mapper;

        public UpsertWeatherForecastHandler(IWeatherForecastRepository weatherForecastRepository,IMapper mapper)
        {
            this._weatherForecastRepository = weatherForecastRepository;
            this._mapper = mapper;
        }

        public async Task<WeatherForecastVM> Handle(UpsertWeatherForecastCommand command, CancellationToken cancellationToken)
        {
            var weatherForecast = _mapper.Map<WeatherForecastTbl>(command);
            var weather = await _weatherForecastRepository.UpsertWeatherForecast(weatherForecast);
            return _mapper.Map<WeatherForecastVM>(weather);
        }
    }

}
