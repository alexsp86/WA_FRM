using WA_FRM.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WA_FRM.Util
{
    public class MONinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMObject>().To<Matrix>();
        }
    }
}