using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ThemeCommons.Selector
{
    /// <summary>
    /// Inspired by https://www.greycastle.se/datatemplate-based-on-type-in-wpf/
    /// </summary>
    public class TemplateByTypeSelector : DataTemplateSelector
    {
        public ObservableCollection<TemplateForType> Templates { get; set; }

        public DataTemplate TemplateForNullItem { get; set; }

        public TemplateByTypeSelector() => Templates = new ObservableCollection<TemplateForType>();

        private DataTemplate FindSingleTValueFor(object item)
        {
            var type = item.GetType();
            var possibleTypes = Templates.Where(mapping => mapping.Type.IsAssignableFrom(type)).ToList();
            if (possibleTypes.Count > 1)
            {
                var typeNames = string.Join(", ", possibleTypes.Select(t => t.Type.Name));
                var errorMessage = string.Format("Item '{0}' can be mapped to multiple types, please specify type TValue mapping more distinctly: " + typeNames, type.Name);
                throw new InvalidOperationException(errorMessage);
            }
            if (possibleTypes.Count == 0)
                throw new InvalidOperationException("No type mapping found for: " + type.Name);
            return possibleTypes[0].DataTemplate;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject obj) => item == null ? NullIfSpecified : FindSingleTValueFor(item);

        protected DataTemplate NullIfSpecified =>
            TemplateForNullItem ?? throw new InvalidOperationException("TemplateForNullItem is not specified");
    }
}
