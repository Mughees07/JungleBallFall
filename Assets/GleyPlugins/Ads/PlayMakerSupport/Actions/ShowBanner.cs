#if USE_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("http://gley.mobi/documentation/Gley-MobileAds-Documentation.pdf")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays a banner")]
    public class ShowBanner : FsmStateAction
    {
        [Tooltip("Location of the banner")]
        public BannerPosition bannerPosition;

        public override void OnEnter()
        {
            Advertisements.Instance.ShowBanner(bannerPosition);
            Finish();
        }
    }
}
#endif
