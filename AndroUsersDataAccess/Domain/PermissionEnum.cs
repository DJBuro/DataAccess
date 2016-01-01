using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroUsersDataAccess.Domain
{
    public enum PermissionEnum
    {
        ViewUsers,
        AddUser,
        EditUser,
        ViewSecurityGroups,
        AddSecurityGroup,
        EditSecurityGroup,
        ViewStores,
        AddStore,
        EditStore,
        ViewPaymentProviders,
        AddPaymentProvider,
        ViewAMSStores,
        EditAMSStore,
        ViewAMSServers,
        AddAMSServer,
        EditAMSServer, // Include delete?
        ViewFTPSites,
        AddFTPSite,
        EditFTPSite, // Include delete?
        ViewACSPartners,
        AddACSPartner,
        EditACSPartner,
        ViewCloudServers,
        ViewAndroAdminLinks
    }
}
