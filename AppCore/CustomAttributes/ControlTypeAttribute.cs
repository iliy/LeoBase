using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.CustomAttributes
{
    public class ControlTypeAttribute:Attribute
    {
        public ControlType ControlType { get; set; }
        public string ValueMember { get; set; }
        public string DisplayMember { get; set; }
        public ControlTypeAttribute(ControlType controlType, string valueMember = null, string displayMember = null)
        {
            ControlType = controlType;
            ValueMember = valueMember;
            DisplayMember = displayMember;
        }
    }

    public class DinamicBrowsableAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public bool PropertyValue { get; set; }
        public DinamicBrowsableAttribute(string propertyName, bool propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }

    public class PropertyNameSelectedTextAttribute:Attribute
    {
        public string PropertyName { get; set; }

        public PropertyNameSelectedTextAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public class BrowsableForEditAndDetailsAttribute:Attribute
    {
        public bool BrowsableForEdit { get; set; }
        public bool BrowsableForDetails { get; set; }
        public BrowsableForEditAndDetailsAttribute(bool browsableForEdit, bool browsableForDetails)
        {
            BrowsableForDetails = browsableForDetails;
            BrowsableForEdit = browsableForEdit;
        }
    }

    public class DataPropertiesNameAttribute : Attribute
    {
        public string PropertyDataName { get; set; }
        public DataPropertiesNameAttribute(string propertyDataName)
        {
            PropertyDataName = propertyDataName;
        }
    }

    public class ControlDataAttribute : Attribute
    {
        public IList Source { get; set; }

        public ControlDataAttribute(IList source)
        {
            Source = source;
        }
    }

    public class ChildrenPropertyAttribute : Attribute
    {
        public string ChildrenPropertyName { get; set; }
        public ChildrenPropertyAttribute(string childrenPropertyName)
        {
            ChildrenPropertyName = childrenPropertyName;
        }
    }

    public class GenerateEmptyModelPropertyNameAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public GenerateEmptyModelPropertyNameAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public class ComboBoxDefaultItem
    {
        public string Value { get; set; }
        public string Display { get; set; }
    }

    public enum ControlType
    {
        Text,
        Int,
        Real,
        ComboBox,
        DateTime,
        Image,
        ImageGallary,
        List
    }


}
