namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class SocialNetworkSiteSettings
    {
        //public SocialNetworkSite Site { get; set; }
        public bool IsEnable { get; set; }

        public bool IsShare { get; set; }

        public bool IsFollow { get; set; }

        public string FollowURL { get; set; }

        //public string FacebookLikeButtonScript { get; set; }

        
    }

    public class FavcebookSocialNetworkSettings : SocialNetworkSiteSettings
    {
        
        public string LikeURL { get; set; }

        public bool EnableFacebookLike { get; set; }
        public bool EnableFacebookActivityFeeds { get; set; }

        //share settings 
        //uses the inept  'IsShare'. 
        public string Caption { get; set; }
        public string Description { get; set; }
    }
}