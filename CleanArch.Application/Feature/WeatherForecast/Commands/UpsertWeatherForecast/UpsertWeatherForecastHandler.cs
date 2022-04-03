using AutoMapper;
using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Application.Common.Exceptions;
using CleanArch.Application.Common.Utils;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastHandler : IRequestHandler<UpsertWeatherForecastCommand, WeatherForecastVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IErrorLocalizer _errorLocaliser;

        public UpsertWeatherForecastHandler(IUnitOfWork unitOfWork, IMapper mapper, IErrorLocalizer errorLocaliser)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._errorLocaliser = errorLocaliser;
        }

        public async Task<WeatherForecastVM> Handle(UpsertWeatherForecastCommand command, CancellationToken cancellationToken)
        {
            if (command != null)
            {
                var weatherForecast = _mapper.Map<WeatherForecastTbl>(command);
                var weather = await _unitOfWork.WeatherForecastRepository.UpsertWeatherForecast(weatherForecast);
                return _mapper.Map<WeatherForecastVM>(weather);
            }

            throw new DataValidationException(_errorLocaliser.InvalidWeatherForecastCommand);
        }
    }
}
