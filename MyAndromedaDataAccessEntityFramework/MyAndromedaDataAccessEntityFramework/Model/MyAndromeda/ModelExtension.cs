using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core.Authorization;
using MyAndromeda.Core.Site;

namespace MyAndromedaDataAccessEntityFramework.Model.MyAndromeda
{
    public partial class Role : IUserRole
    {
        private IEnumerable<IPermission> effectivePermissions;
        public IEnumerable<IPermission> EffectivePermissions 
        {
            get 
            {
                return effectivePermissions ?? (this.RolePermissions.Any() ? this.RolePermissions.Select(e => e.Permission) : Enumerable.Empty<IPermission>());    
            }
            set 
            {
                this.effectivePermissions = value;   
            }
        }
    }

    public partial class Permission : IPermission 
    {
        
    }

    public partial class EnrolmentLevel : IEnrolmentLevel 
    {
    
    }
}
