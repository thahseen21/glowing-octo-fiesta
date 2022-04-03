using AutoMapper;
using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Application.Common.Model;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, PaginatedResultVm<WeatherForecastVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWeatherForecastHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<PaginatedResultVm<WeatherForecastVM>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherForecastList = await _unitOfWork.WeatherForecastRepository.GetWeatherForecastList();
            return _mapper.Map<PaginatedResultVm<WeatherForecastVM>>(weatherForecastList);
        }
    }
}
