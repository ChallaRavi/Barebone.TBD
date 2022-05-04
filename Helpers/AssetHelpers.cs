using Contentful.Core.Images;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barebone.TBD.Helpers
{
    public class AssetHelpers
    {
        public static string BuildImageUrl(Asset asset)
        {
            if (asset == null)
            {
                return string.Empty;
            }

            // imageUrlBuilder = imageUrlBuilder ?? BuildBasicImageProperties();
            var builder = new ImageUrlBuilder();

            // images are given in th form of "//images.ctfassets.net/ekgvz25cv857/4AdnjKUZSgKLeZJlx6sIVS/4809b162a7d3f0950d1e77d7b69e1243/hero_new_desktop.jpg"
            if (!asset.File.Url.Contains("images.ctfassets.net"))
            {
                throw new ArgumentNullException(nameof(asset.File.Url), "No Image Found in Contentful CMS");
            }

            return $"{asset.File.Url}{builder.Build()}";
        }

        /// <summary>
        /// GetEntriesByContentId in the respective Space
        /// </summary>
        /// <param name="format">Image format Option</param>
        /// <param name="height">Height of Image</param>
        /// <param name="width">Width Of Image</param>
        /// <returns>ImageURL</returns>
        public static ImageUrlBuilder BuildBasicImageProperties(ImageFormat format = ImageFormat.Default)
        {
            return ImageUrlBuilder.New().SetFormat(format);
        }

    }
}