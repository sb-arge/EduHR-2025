using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace EduHR.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper'ı projedeki tüm profilleri bulacak şekilde kaydet
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidation'ı projedeki tüm validator'ları bulacak şekilde kaydet
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR'ı projedeki tüm Handler'ları bulacak şekilde kaydet
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Daha önce planladığımız Pipeline Behaviour'ları buraya ekleyeceğiz.
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}