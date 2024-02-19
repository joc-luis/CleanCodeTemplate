using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Authorizations.Input;
using CleanCodeTemplate.Business.Ports.Catalogs.Input;
using CleanCodeTemplate.Business.Ports.Database.Input;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Services.Account;
using CleanCodeTemplate.Business.Services.Authorizations;
using CleanCodeTemplate.Business.Services.Catalogs;
using CleanCodeTemplate.Business.Services.Database;
using CleanCodeTemplate.Business.Services.Locks;
using CleanCodeTemplate.Business.Services.Roles;
using CleanCodeTemplate.Business.Services.Users;

namespace CleanCodeTemplate.Business.Services;

public static class DependencyInjector
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        #region Database

        collection.AddTransient<IInitializeDatabaseInput, InitializeDatabaseService>();

        #endregion

        #region Account

        collection.AddTransient<ICreateAccountInput, CreateAccountService>();
        collection.AddTransient<ILoginAccountInput, LoginAccountService>();
        collection.AddTransient<ITwoFactorLoginAccountInput, TwoFactorLoginAccountService>();
        collection.AddTransient<IRecoverAccountInput, RecoverAccountService>();
        collection.AddTransient<IChangePasswordAccountInput, ChangePasswordAccountService>();
        collection.AddTransient<IGetAccountInput, GetAccountService>();
        collection.AddTransient<IUpdateAccountInput, UpdateAccountService>();
        collection.AddTransient<IUpdatePasswordAccountInput, UpdatePasswordAccountService>();
        collection.AddTransient<IDestroyAccountInput, DestroyAccountService>();

        #endregion

        #region Authorization

        collection.AddScoped<IHaveAuthorizationInput, HaveAuthorizationService>();

        #endregion


        #region Catalogs

        collection.AddTransient<IGetPermissionsCatalogueInput, GetPermissionsCatalogueService>();
        collection.AddTransient<IGetRolesCatalogueInput, GetRolesCatalogueService>();
        collection.AddTransient<IGetUsersCatalogueInput, GetUsersCatalogueService>();

        #endregion

        #region Roles

        collection.AddTransient<ICreateRoleInput, CreateRoleService>();
        collection.AddTransient<IUpdateRoleInput, UpdateRoleService>();
        collection.AddTransient<IDestroyRoleInput, DestroyRoleService>();
        collection.AddTransient<IGetRoleInput, GetRoleService>();

        #endregion
        
        #region Users

        collection.AddTransient<ICreateUserInput, CreateUserService>();
        collection.AddTransient<IUpdateUserInput, UpdateUserService>();
        collection.AddTransient<IDestroyUserInput, DestroyUserService>();
        collection.AddTransient<IGetUserInput, GetUserService>();

        #endregion
        
        #region Users

        collection.AddTransient<ICreateBlockedInput, CreateBlockedService>();
        collection.AddTransient<IUpdateBlockedInput, UpdateBlockedService>();
        collection.AddTransient<IDestroyBlockedInput, DestroyBlockedService>();
        collection.AddTransient<IGetBlockedInput, GetBlockedService>();

        #endregion

        return collection;
    }
}