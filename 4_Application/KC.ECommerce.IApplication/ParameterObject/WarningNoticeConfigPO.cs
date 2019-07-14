namespace KC.ECommerce.IApplication
{
    public class WarningNoticeConfigPO : PagedPO
    {
        public string Type { get; set; }

        public string TypeDescription { get; set; }


        public string SmsAccounts { get; set; }


        public string EmailAccounts { get; set; }
    }
}
