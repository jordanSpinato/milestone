using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using StartingFresh.Models;

namespace StartingFresh.App_Start {

    public class NinjectControllerFactory : DefaultControllerFactory {
        public IKernel Kernel { get; private set; }

        public NinjectControllerFactory(IKernel kernel) {
            this.Kernel = kernel;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType) {
            IController controller = null;

            if (controllerType != null)
                controller = (IController)Kernel.Get(controllerType);

            return controller;
        }

        //private void AddBindings()
        //{
        //    ninjectKernel.Bind <IMilestoneRepository>().ToString<EfMilestoneRepository>();

        //}
    }
}