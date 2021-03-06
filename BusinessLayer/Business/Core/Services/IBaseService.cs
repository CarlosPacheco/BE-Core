﻿using CrossCutting.Security.Identity;
using Microsoft.AspNetCore.Http;

namespace Business.Core.Services
{
    public interface IBaseService<TBusinessLogicObject>
    {
        /// <summary>
        /// Application Request (business layer) logic controller instance
        /// </summary>
        TBusinessLogicObject BusinessLogic { get; }

        /// <summary>
        /// Current HTTP request
        /// </summary>
        HttpRequest Request { get; }

        /// <summary>
        /// Security Principal for authorization information access
        /// </summary>
        IAuthorization Authorization { get; }
    }
}
