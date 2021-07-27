using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice
{
    public class ApiExplorerVersioningWithNamespace : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var versionGroup = controller.ControllerType.Namespace.Split(".").Last();
            if (versionGroup.StartsWith("v"))
            {
                controller.ApiExplorer.GroupName = versionGroup.ToLower();

            }
        }
    }
}
