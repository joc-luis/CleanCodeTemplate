using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;
using CleanCodeTemplate.Business.Ports.Database.Output;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Business.Ports.Users.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Account;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Catalogs;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Database;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Locks;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Roles;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters.Users;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters;

public static class DependencyInjector
{
    public static IServiceCollection AddPresenters(this IServiceCollection collection)
    {
        #region Database

        collection.AddScoped<IInitializeDatabaseOutput, InitializeDatabasePresenter>();

        #endregion

        #region Account

        collection.AddScoped<ICreateAccountOutput, CreateAccountPresenter>();
        collection.AddScoped<ILoginAccountOutput, LoginAccountPresenter>();
        collection.AddScoped<ITwoFactorLoginAccountOutput, TwoFactorLoginAccountPresenter>();
        collection.AddScoped<IRecoverAccountOutput, RecoverAccountPresenter>();
        collection.AddScoped<IChangePasswordAccountOutput, ChangePasswordAccountPresenter>();
        collection.AddScoped<IGetAccountOutput, GetAccountPresenter>();
        collection.AddScoped<IUpdateAccountOutput, UpdateAccountPresenter>();
        collection.AddScoped<IUpdatePasswordAccountOutput, UpdatePasswordAccountPresenter>();
        collection.AddScoped<IDestroyAccountOutput, DestroyAccountPresenter>();

        #endregion

        #region Catalogs

        collection.AddScoped<IGetCatalogueOutput, GetCataloguePresenter>();

        #endregion

        #region Roles

        collection.AddScoped<ICreateRoleOutput, CreateRolePresenter>();
        collection.AddScoped<IUpdateRoleOutput, UpdateRolePresenter>();
        collection.AddScoped<IGetRoleOutput, GetRolePresenter>();
        collection.AddScoped<IDestroyRoleOutput, DestroyRolePresenter>();

        #endregion
        
        
        #region Users

        collection.AddScoped<ICreateUserOutput, CreateUserPresenter>();
        collection.AddScoped<IUpdateUserOutput, UpdateUserPresenter>();
        collection.AddScoped<IGetUserOutput, GetUserPresenter>();
        collection.AddScoped<IDestroyUserOutput, DestroyUserPresenter>();

        #endregion
        
        #region Users

        collection.AddScoped<ICreateBlockedOutput, CreateBlockedPresenter>();
        collection.AddScoped<IUpdateBlockedOutput, UpdateBlockedPresenter>();
        collection.AddScoped<IGetBlockedOutput, GetBlockedPresenter>();
        collection.AddScoped<IDestroyBlockedOutput, DestroyBlockedPresenter>();

        #endregion

        return collection;
    }
}