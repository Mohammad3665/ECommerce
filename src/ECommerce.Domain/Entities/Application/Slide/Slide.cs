using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities.Base;

namespace ECommerce.Domain.Entities.Application.Slide;

/// <summary>
///     Represents a carousel slide or banner image displayed on the website homepage or promotional sections.
/// </summary>
public class Slide : BaseEntity<long>
{
    #region Content Information

    /// <summary>
    ///     Gets or sets the main heading or caption of the slide.
    /// </summary>
    /// <value>
    ///     A string containing the slide's primary text. Defaults to empty string.
    /// </value>
    /// <remarks>
    ///     Displayed prominently on the banner image as a headline.
    ///     Best practices:
    ///     - Keep it short and impactful (5-10 words maximum)
    ///     - Use action-oriented language (e.g., "Summer Sale Starts Now")
    ///     - Should be readable even on small screens
    ///     - May be omitted for image-only slides
    ///     
    ///     Examples:
    ///     - "New Collection 2024"
    ///     - "Up to 50% Off"
    ///     - "Free Shipping on Orders Over $50"
    /// </remarks>
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the supporting text or subheading of the slide.
    /// </summary>
    /// <value>
    ///     A string containing additional details or call-to-action text. Defaults to empty string.
    /// </value>
    /// <remarks>
    ///     Displayed below the title as secondary text.
    ///     Best practices:
    ///     - Provides more context or details about the promotion
    ///     - Typically 1-2 sentences (15-30 words)
    ///     - Can include discount details, product features, or deadlines
    ///     - May be omitted for minimalist designs
    ///     
    ///     Examples:
    ///     - "Shop the latest trends and save big"
    ///     - "Get free shipping on all orders over $100"
    ///     - "Limited stock available - order now!"
    /// </remarks>
    [Required]
    public string Description { get; set; } = string.Empty;

    #endregion

    #region Media & Navigation

    /// <summary>
    ///     Gets or sets the URL or file path of the slide background or banner image.
    /// </summary>
    /// <value>
    ///     A string representing the path to the slide image. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the destination URL when the slide is clicked.
    /// </summary>
    /// <value>
    ///     A string containing the relative or absolute URL to navigate to. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string Link { get; set; } = string.Empty;

    #endregion

    #region Display Configuration

    /// <summary>
    ///     Gets or sets the sorting order of the slide within the carousel.
    /// </summary>
    /// <value>
    ///     An integer representing the display sequence (lower values appear first). Default is 0.
    /// </value>
    public int DisplayOrder { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the slide is visible on the website.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the slide is displayed in the carousel; otherwise, <c>false</c>. Default is <c>true</c>.
    /// </value>
    public bool IsActive { get; set; } = true;

    #endregion
}