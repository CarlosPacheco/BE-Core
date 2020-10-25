using System;
using System.Data;
using System.Web;
using CrossCutting.Security.Identity;
using Business.Core.Data;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.AspNetCore.Http;

namespace Business.Core
{
    //public abstract class IdentityBaseBLO<TEntity, TIDataAccessObject, TIdentityProvider, TIdentityUserManager, TIdentityRoleManager> : BaseBLO<TEntity, TIDataAccessObject>
    //    where TEntity : class, IEntity, new()
    //    where TIDataAccessObject : IBaseDAO, new()
    //   // where TIdentityProvider : IIdentityProvider, new()
    //{
    //    //private readonly Lazy<TIdentityUserManager> _userManager = new Lazy<TIdentityUserManager>(HttpContext.Current.GetOwinContext().GetUserManager<TIdentityUserManager>);
    //    //private readonly Lazy<TIdentityRoleManager> _roleManager = new Lazy<TIdentityRoleManager>(HttpContext.Current.GetOwinContext().GetUserManager<TIdentityRoleManager>);
    //    //protected TIdentityUserManager UserManager => _userManager.Value;
    //    //protected TIdentityRoleManager RoleManager => _roleManager.Value;

    //    //protected IdentityBaseBLO()
    //    //{
    //    //    DataAccess = new TIDataAccessObject();
    //    //    IdentityProvider = new TIdentityProvider();
    //    //}

    //    public IdentityBaseBLO(TIDataAccessObject dataAccess, IIdentityProvider identityProvider) : base(dataAccess, identityProvider)
    //    {
    //        DataAccess = dataAccess;
    //        IdentityProvider = identityProvider;
    //    }

    //    public IdentityBaseBLO(TIDataAccessObject dataAccess, IIdentityProvider identityProvider, TIdentityUserManager userManager, TIdentityRoleManager roleManager) : base(dataAccess, identityProvider)
    //    {
    //        DataAccess = dataAccess;
    //        IdentityProvider = identityProvider;
    //    }

    //    //public IdentityBaseBLO(IIdentityProvider identityProvider, IDbTransaction dbTransaction)
    //    //{
    //    //    DataAccess = new TIDataAccessObject();
    //    //    IdentityProvider = identityProvider;

    //    //    // <remarks> On why through a setter method:
    //    //    // Calling TIDataAccessObject constructor with reflection or Instance Activator has a great overhead,
    //    //    //  so let's not do that! 
    //    //    // Calling the constructor with property initialization (new TIDataAccessObject(){ Prop = value; }) could break
    //    //    //  some future logic implemented in the TIDataAccessObject constructor, let's avoid this too!
    //    //    // Let's inject the transaction through a setter method insted, avoids overheads and confusions and it's simple.
    //    //    DataAccess.SetDbTransaction(dbTransaction);
    //    //}

    //    //public IdentityBaseBLO(IIdentityProvider identityProvider, IDbConnection dbConnection)
    //    //{
    //    //    DataAccess = new TIDataAccessObject();
    //    //    IdentityProvider = identityProvider;

    //    //    DataAccess.SetDbConnection(dbConnection);
    //    //}
    //}
}
