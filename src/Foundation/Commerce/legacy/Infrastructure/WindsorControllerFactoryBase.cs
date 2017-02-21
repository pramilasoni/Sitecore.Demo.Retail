﻿//-----------------------------------------------------------------------
// <copyright file="WindsorControllerFactoryBase.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the WindsorControllerFactory class.</summary>
//-----------------------------------------------------------------------
// Copyright 2015 Sitecore Corporation A/S
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
// except in compliance with the License. You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the 
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
// either express or implied. See the License for the specific language governing permissions 
// and limitations under the License.
// -------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.MicroKernel;
using Sitecore.Foundation.Commerce.Managers;

namespace Sitecore.Reference.Storefront.Infrastructure
{
    public abstract class WindsorControllerFactoryBase : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        protected WindsorControllerFactoryBase(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            if (IsFromCurrentAssembly(controller.GetType()))
            {
                _kernel.ReleaseComponent(controller);
            }
            else
            {
                base.ReleaseController(controller);
            }
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (IsFromCurrentAssembly(controllerType))
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, $"The controller for path '{requestContext.HttpContext.Request.Path}' could not be found.");
                }

                return (IController) _kernel.Resolve(controllerType);
            }

            var controller = base.GetControllerInstance(requestContext, controllerType);

            return controller;
        }

        protected abstract bool IsFromCurrentAssembly(Type type);

        protected override SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, Type controllerType)
        {
            return SessionStateBehavior.Required;
        }
    }
}