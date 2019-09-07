using HgpStaff3D.Application.MediatRCommands;
using System;

namespace HgpStaff3D
{
    public static class MediatRConfigure
    {
        public static Type[] HandlerAssemblyMarkerTypes()
        {
            var type = new []
            {
                typeof(CreateDepartmentHandler)
            };
            return type;
        }
    }
}
