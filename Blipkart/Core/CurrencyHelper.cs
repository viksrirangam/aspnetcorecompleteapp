using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blipkart.Core.UI
{
    [HtmlTargetElement(Attributes = CurrencyAttributeName)]
    public class CurrencyTagHelper : TagHelper
    {
        private const string CurrencyAttributeName = "currency";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = @"<span>Rs. </span>";

            output.PreContent.AppendHtml(content);

            if (output.Attributes.ContainsName(CurrencyAttributeName))
            {
                output.Attributes.RemoveAll(CurrencyAttributeName);
            }

            base.Process(context, output);
        }
    }
}
