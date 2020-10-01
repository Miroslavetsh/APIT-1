using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Apit.Service
{
    public class AdminAuthorization : IControllerModelConvention
    {
        private readonly string _area;
        private readonly string _policy;

        public AdminAuthorization(string area, string policy)
        {
            _area = area;
            _policy = policy;
        }

        public void Apply(ControllerModel controller)
        {
            const StringComparison sc = StringComparison.OrdinalIgnoreCase;
            if (controller.Attributes.Any(attr =>
                    attr is AreaAttribute attribute && attribute.RouteValue.Equals(_area, sc))
                || controller.RouteValues.Any(route =>
                    route.Key.Equals("area", sc) && route.Value.Equals(_area, sc)))
            {
                controller.Filters.Add(new AuthorizeFilter(_policy));
            }
        }
    }
}