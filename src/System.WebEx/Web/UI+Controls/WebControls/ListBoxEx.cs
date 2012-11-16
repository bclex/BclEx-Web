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
using System.Patterns.UI.Forms;
using System.Text;
namespace System.Web.UI.WebControls
{
    /// <summary>
    /// ListBoxEx
    /// </summary>
    public class ListBoxEx : ListBox, IFormControl
    {
        private string _staticTextSeparator = ", ";

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>
        /// The view mode.
        /// </value>
        public FormFieldViewMode ViewMode { get; set; }

        #region Option-Groups

        /// <summary>
        /// Creates the begin group list item.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static ListItem CreateBeginGroupListItem(string text)
        {
            var listItem = new ListItem(text);
            listItem.Attributes["group"] = "begin";
            return listItem;
        }

        /// <summary>
        /// Creates the end group list item.
        /// </summary>
        /// <returns></returns>
        public static ListItem CreateEndGroupListItem()
        {
            var listItem = new ListItem();
            listItem.Attributes["group"] = "end";
            return listItem;
        }

        //public string DataGroupPathField { get; set; }

        //protected override void PerformDataBinding(IEnumerable dataSource)
        //{
        //    if (dataSource != null)
        //        throw new ArgumentNullException("dataSource");
        //    string dataGroupPathField = DataGroupPathField;
        //    if (string.IsNullOrEmpty(dataGroupPathField))
        //        base.PerformDataBinding(dataSource);
        //}

        //[Match("match with ListBox RenderContents")]
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="w">The w.</param>
        protected override void RenderContents(HtmlTextWriter w)
        {
            var itemHash = Items;
            var itemCount = itemHash.Count;
            if (itemCount > 0)
            {
                var isA = false;
                for (int itemKey = 0; itemKey < itemCount; itemKey++)
                {
                    var listItem = itemHash[itemKey];
                    if (listItem.Enabled)
                        switch (listItem.Attributes["group"])
                        {
                            case "begin":
                                w.WriteBeginTag("optgroup");
                                w.WriteAttribute("label", listItem.Text);
                                w.Write('>');
                                break;
                            case "end":
                                w.WriteEndTag("optgroup");
                                break;
                            default:
                                w.WriteBeginTag("option");
                                if (listItem.Selected)
                                {
                                    if (isA)
                                        VerifyMultiSelect();
                                    isA = true;
                                    w.WriteAttribute("selected", "selected");
                                }
                                w.WriteAttribute("value", listItem.Value, true);
                                if (listItem.Attributes.Count > 0)
                                    listItem.Attributes.Render(w);
                                if (Page != null)
                                    Page.ClientScript.RegisterForEventValidation(UniqueID, listItem.Value);
                                w.Write('>');
                                HttpUtility.HtmlEncode(listItem.Text, w);
                                w.WriteEndTag("option");
                                w.WriteLine();
                                break;
                        }
                }
            }
        }

        #endregion

        /// <summary>
        /// Renders the specified w.
        /// </summary>
        /// <param name="w">The w.</param>
        protected override void Render(HtmlTextWriter w)
        {
            switch (ViewMode)
            {
                case FormFieldViewMode.Static:
                    RenderStaticText(w);
                    break;
                case FormFieldViewMode.StaticWithHidden:
                    RenderStaticText(w);
                    RenderHidden(w);
                    break;
                case FormFieldViewMode.Hidden:
                    RenderHidden(w);
                    break;
                default:
                    base.Render(w);
                    break;
            }
        }

        /// <summary>
        /// Renders the hidden.
        /// </summary>
        /// <param name="w">The w.</param>
        protected void RenderHidden(HtmlTextWriter w)
        {
            var b = new StringBuilder();
            foreach (ListItem item in Items)
                if (item.Selected)
                    b.Append(item.Value + ",");
            if (b.Length > 0)
                b.Length--;
            w.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            w.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            w.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            w.AddAttribute(HtmlTextWriterAttribute.Value, b.ToString());
            w.RenderBeginTag(HtmlTextWriterTag.Input);
            w.RenderEndTag();
        }

        /// <summary>
        /// Renders the static text.
        /// </summary>
        /// <param name="w">The w.</param>
        protected virtual void RenderStaticText(HtmlTextWriter w)
        {
            var staticTextSeparator = StaticTextSeparator;
            var b = new StringBuilder();
            foreach (ListItem item in Items)
                if (item.Selected)
                    b.Append(HttpUtility.HtmlEncode(item.Text) + staticTextSeparator);
            var staticTextSeparatorLength = staticTextSeparator.Length;
            if (b.Length > staticTextSeparatorLength)
                b.Length -= staticTextSeparatorLength;
            w.AddAttribute(HtmlTextWriterAttribute.Class, "static");
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            w.Write(b.ToString());
            w.RenderEndTag();
        }

        /// <summary>
        /// Gets or sets the static text separator.
        /// </summary>
        /// <value>
        /// The static text separator.
        /// </value>
        public string StaticTextSeparator
        {
            get { return _staticTextSeparator; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _staticTextSeparator = value;
            }
        }
    }
}
