using AutoMapper;
using Business.Core;
using CrossCutting.Security.Identity;
using Microsoft.AspNetCore.Http;

namespace CrossCutting.Web.Services
{
    /// <summary>
    /// Base api controller's service class
    /// </summary>
    /// <typeparam name="TBusinessLogicObject"></typeparam>
    public class BaseService<TBusinessLogicObject> : IBaseService<TBusinessLogicObject> where TBusinessLogicObject : IBaseBlo
    {
        /// <summary>
        /// Application Request (business layer) logic controller instance
        /// </summary>
        public TBusinessLogicObject BusinessLogic { get; protected set; }

        /// <summary>
        /// Mapper instance
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// Current HTTP request
        /// </summary>
        public HttpRequest Request { get; }

        /// <summary>
        /// Security Principal for authorization information access
        /// </summary>
        public IAuthorization Authorization { get; }

        public BaseService(TBusinessLogicObject businessLogic, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAuthorization authorization)
        {
            BusinessLogic = businessLogic;
            Request = httpContextAccessor.HttpContext.Request;
            Mapper = mapper;
            Authorization = authorization;  
        }
    }

}