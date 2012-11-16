#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Contoso.Web.UI.WebControls
{
    /// <summary>
    /// CopyrightLiteral
    /// </summary>
    public class CopyrightLiteral : Literal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyrightLiteral"/> class.
        /// </summary>
        public CopyrightLiteral() { BeginYear = 0; }

        /// <summary>
        /// Renders the specified w.
        /// </summary>
        /// <param name="w">The w.</param>
        protected override void Render(HtmlTextWriter w)
        {
            int beginYear = BeginYear;
            int currentYear = DateTime.Now.Year;
            string yearText = (beginYear > 0 ? beginYear.ToString(CultureInfo.InvariantCulture) : string.Empty);
            if (currentYear > beginYear)
                yearText = StringEx.Axb(yearText, "-", currentYear.ToString(CultureInfo.InvariantCulture));
            Text = Text.Replace("{0}", yearText);
            base.Render(w);
        }

        /// <summary>
        /// Gets or sets the begin year.
        /// </summary>
        /// <value>
        /// The begin year.
        /// </value>
        public int BeginYear { get; set; }
    }
}
