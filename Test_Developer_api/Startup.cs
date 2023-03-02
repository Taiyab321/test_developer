using AutoMapper;
using Test_Developer_api.Services;
using Test_Developer_api.Services.Interfaces;
using Test_Developer_api.Mapper;
using Test_Developer_api.Configuration;

namespace Test_Developer_api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
        });
        services.AddControllers();
        services.AddScoped<IPostCodeService, PostCodeService>();
        services.AddEndpointsApiExplorer();
        services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new DevAutoMapper());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseCors();

        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            //endpoints.MapGet("/", async context =>
            //{
            //    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            //});
        });
    }
}