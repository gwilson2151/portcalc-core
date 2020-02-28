using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PortfolioSmarts.Domain.Service;

namespace PortfolioSmarts.PortfolioApp.Services
{
	public class ServiceFactory
	{
		private readonly IServiceProvider _serviceProvider;
		public ServiceFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IService GetService(ServiceDefinition serviceDefinition)
		{
			switch (serviceDefinition.Type)
			{
			case "Questrade":
				var service = _serviceProvider.GetService<Questrade.Interfaces.IQuestradeApi>() as IService;
				var task = service.Initialise(serviceDefinition);
				task.Wait();
				return service;
			}
			throw new NotImplementedException();
		}
	}
}
