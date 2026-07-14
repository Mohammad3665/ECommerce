namespace ECommerce.Application.Authorization;

/// <summary>
///     Contains all permission constants used throughout the application for authorization.
/// </summary>
/// <remarks>
///     These permission names are used with the <see cref="HasPermissionAttribute"/> 
///     to secure controllers and actions. Each permission follows the pattern: 
///     "{resource}.{action}" (e.g., "users.create").
/// </remarks>
public static class Permissions
{
    /// <summary>
    ///     User management permissions.
    /// </summary>
    public static class Users
    {
        /// <summary>Permission to create new users.</summary>
        public const string Create = "users.create";

        /// <summary>Permission to view user details and list users.</summary>
        public const string Read = "users.read";

        /// <summary>Permission to update existing user information.</summary>
        public const string Update = "users.update";

        /// <summary>Permission to delete users from the system.</summary>
        public const string Delete = "users.delete";
    }

    /// <summary>
    ///     Role management permissions.
    /// </summary>
    public static class Roles
    {
        /// <summary>Permission to create new roles.</summary>
        public const string Create = "roles.create";

        /// <summary>Permission to view role details and list roles.</summary>
        public const string Read = "roles.read";

        /// <summary>Permission to update existing role information and permissions.</summary>
        public const string Update = "roles.update";

        /// <summary>Permission to delete roles from the system.</summary>
        public const string Delete = "roles.delete";
    }

    /// <summary>
    ///     Product management permissions.
    /// </summary>
    public static class Products
    {
        /// <summary>Permission to add new products to the catalog.</summary>
        public const string Create = "products.create";

        /// <summary>Permission to view product details and list products.</summary>
        public const string Read = "products.read";

        /// <summary>Permission to modify existing product information.</summary>
        public const string Update = "products.update";

        /// <summary>Permission to remove products from the catalog.</summary>
        public const string Delete = "products.delete";
    }

    public static class Categories
    {
        /// <summary>Permission to create new categories.</summary>
        public const string Create = "categories.create";

        /// <summary>Permission to view category details and list categories.</summary>
        public const string Read = "categories.read";

        /// <summary>Permission to update existing category information.</summary>
        public const string Update = "categories.update";

        /// <summary>Permission to delete categories from the system.</summary>
        public const string Delete = "categories.delete";
    }

    /// <summary>
    ///     Brand management permissions.
    /// </summary>
    public static class Brands
    {
        /// <summary>Permission to create new brands.</summary>
        public const string Create = "brands.create";

        /// <summary>Permission to view brand details and list brands.</summary>
        public const string Read = "brands.read";

        /// <summary>Permission to update existing brand information.</summary>
        public const string Update = "brands.update";

        /// <summary>Permission to delete brands from the system.</summary>
        public const string Delete = "brands.delete";
    }

    /// <summary>
    ///     Order management permissions.
    /// </summary>
    public static class Orders
    {
        /// <summary>Permission to view order details and list orders.</summary>
        public const string Read = "orders.read";

        /// <summary>Permission to modify order information (e.g., status, items).</summary>
        public const string Update = "orders.update";

        /// <summary>Permission to cancel existing orders.</summary>
        public const string Cancel = "orders.cancel";
    }


    /// <summary>
    ///     Comment/Review management permissions.
    /// </summary>
    public static class Comments
    {
        /// <summary>Permission to view comments and reviews.</summary>
        public const string Read = "comments.read";

        /// <summary>Permission to approve pending comments.</summary>
        public const string Approve = "comments.approve";

        /// <summary>Permission to reject inappropriate comments.</summary>
        public const string Reject = "comments.reject";

        /// <summary>Permission to delete comments from the system.</summary>
        public const string Delete = "comments.delete";
    }

    /// <summary>
    ///     Article/Blog management permissions.
    /// </summary>
    public static class Articles
    {
        /// <summary>Permission to view article details and list articles.</summary>
        public const string Read = "articles.read";

        /// <summary>Permission to create new articles.</summary>
        public const string Create = "articles.create";

        /// <summary>Permission to modify existing articles.</summary>
        public const string Update = "articles.update";

        /// <summary>Permission to delete articles from the system.</summary>
        public const string Delete = "articles.delete";
    }

    /// <summary>
    ///     Slider/Banner management permissions.
    /// </summary>
    public static class Sliders
    {
        /// <summary>Permission to view sliders and banners.</summary>
        public const string Read = "sliders.read";

        /// <summary>Permission to create new sliders or banners.</summary>
        public const string Create = "sliders.create";

        /// <summary>Permission to modify existing sliders.</summary>
        public const string Update = "sliders.update";

        /// <summary>Permission to delete sliders from the system.</summary>
        public const string Delete = "sliders.delete";
    }

    /// <summary>
    ///     Coupon/Discount management permissions.
    /// </summary>
    public static class Coupons
    {
        /// <summary>Permission to view coupon details and list coupons.</summary>
        public const string Read = "coupons.read";

        /// <summary>Permission to create new discount coupons.</summary>
        public const string Create = "coupons.create";

        /// <summary>Permission to modify existing coupon settings.</summary>
        public const string Update = "coupons.update";

        /// <summary>Permission to delete coupons from the system.</summary>
        public const string Delete = "coupons.delete";
    }

    /// <summary>
    ///     Dashboard access permissions.
    /// </summary>
    public static class Dashboard
    {
        /// <summary>Permission to access and view the administrative dashboard.</summary>
        public const string View = "dashboard.view";
    }
}
