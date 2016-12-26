using System;
using System.Text.RegularExpressions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.SpecDisplay
{
    public partial class SpecControl : BaseExtendedControl<SpecModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var specModel = model as SpecModel;
            if (specModel == null)
                return;

            var index = 10;
            var pattern = @"\<p (?<attr>.*?)\>(?<text>.*?)\</p\>";

            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

            dvFullText.InnerHtml = regex.Replace(specModel.FullText, n => Replacer(n, index++));
        }

        protected String Replacer(Match match, int index)
        {
            var attr = match.Groups["attr"].Value;
            var text = match.Groups["text"].Value;

            var result = String.Format("<p tabindex='{0}' class='listenOnFocus' {1}>{2}</p>", index, attr, text);
            return result;
        }
    }
}