namespace KC.ECommerce.Common
{
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        AuthorizedUser CurrentUser { get; }
    }
}
