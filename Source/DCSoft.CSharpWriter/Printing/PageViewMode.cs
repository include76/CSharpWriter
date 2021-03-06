﻿/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.Printing
{
    /// <summary>
    /// 页面显示方式
    /// </summary>
    public enum PageViewMode
    {
        /// <summary>
        /// 普通方式
        /// </summary>
        Normal,
        /// <summary>
        /// 页面方式
        /// </summary>
        Page,
        /// <summary>
        /// 压缩页面方式
        /// </summary>
        CompressPage
    }
}
