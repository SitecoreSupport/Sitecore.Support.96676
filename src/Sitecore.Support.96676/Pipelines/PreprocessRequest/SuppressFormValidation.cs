// --------------------------------------------------------------------------------------------------------------------
// <copyright file="004 - SuppressFormValidation.cs" company="Sitecore">
//   Copyright (c) Sitecore. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Support.Pipelines.PreprocessRequest
{
    using Diagnostics;
    using Sitecore.Pipelines.PreprocessRequest;
    using System;
    using System.Web;
    using Sites;

    /// <summary>
    /// The suppress form validation.
    /// </summary>
    public class SuppressFormValidation : PreprocessRequestProcessor
    {
        /// <summary>
        /// Suppresses the form validation excheption that has been introduced in .NET 4.0 for Sitecore backend.
        /// </summary>
        /// <param name="args">The pipeline arguments.</param>
        /// <exception cref="HttpRequestValidationException">Indicates that form validation exception occured for frontend.</exception>
        public override void Process([NotNull] PreprocessRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            try
            {
                var form = args.Context.Request.Form;
            }
            catch (HttpRequestValidationException)
            {
                string rawUrl = args.Context.Request.RawUrl;

                if (!(((rawUrl.StartsWith("/sitecore/shell/", StringComparison.InvariantCultureIgnoreCase) || rawUrl.StartsWith("/sitecore/admin/", StringComparison.InvariantCultureIgnoreCase)) || rawUrl.StartsWith(SiteManager.GetSite("shell").Properties["loginPage"], StringComparison.InvariantCultureIgnoreCase)) || rawUrl.StartsWith("/-/speak/request/", StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw;
                }
            }
        }
    }
}
