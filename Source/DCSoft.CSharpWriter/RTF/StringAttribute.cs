﻿/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */

using System;
using System.Text;

namespace DCSoft.RTF
{
    /// <summary>
    /// string attribute
    /// </summary>
    [Serializable()]
    public class StringAttribute
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public StringAttribute()
        {
        }

        private string strName = null;
        /// <summary>
        /// name
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Name
        {
            get
            {
                return strName; 
            }
            set 
            {
                strName = value; 
            }
        }

        private string strValue = null;
        /// <summary>
        /// value
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Value
        {
            get
            {
                return strValue; 
            }
            set
            {
                strValue = value; 
            }
        }
        public override string ToString()
        {
            return strName + "=" + strValue;
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerTypeProxy(typeof(RTFInstanceDebugView))]
    public class StringAttributeCollection : System.Collections.CollectionBase
    {
        public StringAttributeCollection()
        {
        }

        public string this[string name]
        {
            get
            {
                foreach (StringAttribute attr in this)
                {
                    if (attr.Name == name)
                    {
                        return attr.Value;
                    }
                }
                return null;
            }
            set
            {
                foreach (StringAttribute item in this)
                {
                    if (item.Name == name)
                    {
                        if (value == null)
                            this.List.Remove(item);
                        else
                            item.Value = value;
                        return;
                    }
                }
                if (value != null)
                {
                    StringAttribute newItem = new StringAttribute();
                    newItem.Name = name;
                    newItem.Value = value;
                    this.List.Add(newItem);
                }
            }
        }

        public int Add(StringAttribute item)
        {
            return this.List.Add(item);
        }

        public void Remove(StringAttribute item)
        {
            this.List.Remove(item);
        }
    }
}
