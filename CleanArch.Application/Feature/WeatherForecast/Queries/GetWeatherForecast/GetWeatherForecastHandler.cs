﻿using AutoMapper;
using CleanArch.Application.Common.Contracts.Repositories;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, List<WeatherForecastVM>>
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IMapper _mapper;

        public GetWeatherForecastHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper)
        {
            this._weatherForecastRepository = weatherForecastRepository;
            this._mapper = mapper;
        }

        public async Task<List<WeatherForecastVM>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherForecastList = await _weatherForecastRepository.GetWeatherForecastList();
            return _mapper.Map<List<WeatherForecastVM>>(weatherForecastList);
        }
    }
}