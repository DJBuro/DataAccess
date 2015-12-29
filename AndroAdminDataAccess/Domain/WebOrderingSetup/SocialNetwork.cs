namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class SocialNetwork
    {
        public SocialNetworkSiteSettings FacebookSettings { get; set; }

        public SocialNetworkSiteSettings TwitterSettings { get; set; }

        public SocialNetworkSiteSettings InstagramSettings { get; set; }

        public SocialNetworkSiteSettings PinterestSettings { get; set; }       

        //public List<SocialNetworkSiteSettings> Settings { get; set; }
        public void DefaultSocialNetwork()
        { 
            this.FacebookSettings = new SocialNetworkSiteSettings { IsEnable = true, IsShare = true };
            this.TwitterSettings = new SocialNetworkSiteSettings { IsEnable = true, IsShare = true };
        }
    }
}