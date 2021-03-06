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

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 可用状态
    /// </summary>
    public enum EnableState
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default ,
        /// <summary>
        /// 可用状态
        /// </summary>
        Enabled ,
        /// <summary>
        /// 无效状态
        /// </summary>
        Disabled
    }
}
