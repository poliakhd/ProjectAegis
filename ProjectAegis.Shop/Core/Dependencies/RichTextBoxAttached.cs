using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProjectAegis.Shop.Core.Dependencies
{
    public class RichTextBoxAttached
    {
        public static readonly DependencyProperty BindableDocumentProperty =
            DependencyProperty.RegisterAttached("BindableDocument", typeof(FlowDocument), typeof(RichTextBoxAttached), new PropertyMetadata(Callback));

        public static FlowDocument GetBindableDocument(DependencyObject source)
        {
            return (FlowDocument)source.GetValue(BindableDocumentProperty);
        }
        public static void SetBindableDocument(DependencyObject source, FlowDocument value)
        {
            source.SetValue(BindableDocumentProperty, value);
        }

        private static void Callback(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var richTextBox = source as RichTextBox;

            if (args.NewValue == null)
            {
                richTextBox.Document = new FlowDocument();
                return;
            }

            richTextBox.Document = args.NewValue as FlowDocument;
        }
    }
}