namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class SocialNetworkSiteSettings
    {
        //public SocialNetworkSite Site { get; set; }
        public bool IsEnable { get; set; }

        public bool IsShare { get; set; }

        public bool IsFollow { get; set; }

        public string FollowURL { get; set; }

        public bool EnableFacebookLike { get; set; }
    }
}