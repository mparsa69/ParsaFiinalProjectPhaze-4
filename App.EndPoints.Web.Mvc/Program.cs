using App.Domain.AppServices.BaseService;
using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Contracts.IRepositories;
using App.Domain.Core.BaseService.Contracts.IServices;
using App.Domain.Services.BaseService;
using App.Infrastructures.Database.SqlServer.Data;
using App.Infrastructures.Repositories.EfCore.BaseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.OrderAgg.Contracts.IAppServices;
using App.Domain.AppServices.Order;
using App.Domain.Services.Order;
using App.Domain.Core.OrderAgg.Contracts.IServices;
using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Infrastructures.Repositories.EfCore.OrderRepo;
using App.Domain.Core.SuggestionAgg.Contracts.IAppServices;
using App.Domain.AppServices.Suggestion;
using App.Domain.Core.SuggestionAgg.Contracts.IServices;
using App.Domain.Services.Suggestion;
using App.Domain.Core.SuggestionAgg.Contracts.IRepositories;
using App.Infrastructures.Repositories.EfCore.SuggestionRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
/*builder.Services.AddRazorPages().AddRazorRuntimeCompilation();*/
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
});

/*builder.Services.ConfigureApplicationCookie(options =>
{

    options.AccessDeniedPath = "~/AccessDenied.html";

});*/
/*builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();*/
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();*/
#region BaseService
//MainCategory Services
builder.Services.AddScoped<IMainCategoryAppService, MainCategoryAppService>();
builder.Services.AddScoped<IMainCategoryService, MainCategoryService>();
builder.Services.AddScoped<IMainCategoryCommandRepository, MainCategoryCommandRepository>();
builder.Services.AddScoped<IMainCategoryQueryRepository, MainCategoryQueryRepository>();
//SecondaryCategory Services
builder.Services.AddScoped<ISecondaryCategoryAppService, SecondaryCategoryAppService>();
builder.Services.AddScoped<ISecondaryCategoryService, SecondaryCategoryService>();
builder.Services.AddScoped<ISecondaryCategoryCommandRepository, SecondaryCategoryCommandRepository>();
builder.Services.AddScoped<ISecondaryCategoryQueryRepository, SecondaryCategoryQueryRepository>();
//ThirdCategory Services
builder.Services.AddScoped<IThirdCategoryAppService, ThirdCategoryAppService>();
builder.Services.AddScoped<IThirdCategoryService, ThirdCategoryService>();
builder.Services.AddScoped<IThirdCategoryCommandRepository, ThirdCategoryCommandRepository>();
builder.Services.AddScoped<IThirdCategoryQueryRepository, ThirdCategoryQueryRepository>();
#endregion
#region Order
builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
#endregion

#region Suggestion
builder.Services.AddScoped<ISuggestionAppService, SuggestionAppService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ISuggestionCommandRepository, SuggestionCommandRepository>();
builder.Services.AddScoped<ISuggestionQueryRepository, SuggestionQueryRepository>();
#endregion

#region Comment
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentCommandRepository, CommentCommandRepository>();
builder.Services.AddScoped<ICommentQueryRepository, CommentQueryRepository>();

#endregion

builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();
