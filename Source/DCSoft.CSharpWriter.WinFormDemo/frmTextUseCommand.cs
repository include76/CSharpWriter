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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Controls;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Html;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Data;
using DCSoft.CSharpWriter.Security;
using DCSoft.Common;
using DCSoft.Data;
using System.IO;

namespace DCSoft.CSharpWriter.WinFormDemo
{
    /// <summary>
    /// Demo of DCSoft.CSharpWriter
    /// </summary>
    public partial class frmTextUseCommand : Form
    {
        public frmTextUseCommand()
        {
            InitializeComponent();
            //myEditControl.Enabled = false;
            myEditControl.ServerObject = new ServerObjectSample(this);
            myEditControl.DoubleBuffering = false;
            //myEditControl.ViewMode = Printing.PageViewMode.Normal;
            //myEditControl.Document.Options.EditOptions.AutoEditElementValue = true;
            //myEditControl.IsAdministrator = true;
            //myEditControl.Readonly = true;
           
             //myEditControl.IsAdministrator = true;
            //myEditControl.DocumentControler.DataFilter = new MyDataFilter();
            //myEditControl.HeaderFooterReadonly = true;
            myEditControl.AllowDragContent = true;
            //DocumentContentStyle style = myEditControl.Document.Style;
            //myEditControl.AutoSetDocumentDefaultFont = false;
              
        }

        private void frmTextUseCommand_Load(object sender, EventArgs e)
        {
            //DCSoft.CSharpWriter.Controls.TextWindowsFormsEditorHost.PopupFormSizeFix = new System.Drawing.Size(40, 20);
            myEditControl.Font = new Font(System.Windows.Forms.Control.DefaultFont.Name, 12);

            // 设置编辑器界面双缓冲
            myEditControl.DoubleBuffering = true;// _StartOptions.DoubleBuffering;
            // init command controler
            myEditControl.CommandControler = myCommandControler;
            myCommandControler.Start();

            myEditControl.DocumentOptions = new DocumentOptions();
            // 设置文档处于调试模式
            myEditControl.DocumentOptions.BehaviorOptions.DebugMode = true;

            // Without permission control
            myEditControl.DocumentOptions.SecurityOptions.EnableLogicDelete = false;
            myEditControl.DocumentOptions.SecurityOptions.EnablePermission = false;
            myEditControl.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = false;
            myEditControl.DocumentOptions.SecurityOptions.ShowPermissionMark = false;
            
            myEditControl.AutoUserLogin = false;

            this.myEditControl_DocumentLoad(null, null);

            var dir = Path.Combine(Application.StartupPath, "DemoFile");
            if (Directory.Exists(dir))
            {
                var fns = Directory.GetFiles(dir, "*.xml");
                if (fns != null && fns.Length > 0)
                {
                    btnDemoFiles.Visible = true;
                    foreach (var fn in fns)
                    {
                        var menu = new System.Windows.Forms.ToolStripMenuItem(Path.GetFileName(fn));
                        menu.Tag = fn;
                        menu.Click += delegate (object sender2, EventArgs args2)
                        {
                            var menuItem = (System.Windows.Forms.ToolStripMenuItem)sender2;
                            var fn2 = (string)menuItem.Tag;
                            if (File.Exists(fn2))
                            {
                                this.Cursor = Cursors.WaitCursor;
                                this.myEditControl.LoadDocument(fn2, FileFormat.XML);
                                this.Cursor = Cursors.Default;
                                //this.myEditControl.ExecuteCommand("FileOpen", true, fn2);
                            }
                        };
                        btnDemoFiles.DropDownItems.Add(menu);
                    }
                }
            }
        }

         
        /// <summary>
        /// Handle after load document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_DocumentLoad(object sender, EventArgs e)
        {
           
        } 
         
        /// <summary>
        /// Demo of server object in document
        /// </summary>
        public class ServerObjectSample
        {
            public ServerObjectSample(frmTextUseCommand frm)
            {
                _Form = frm;
            }

            private frmTextUseCommand _Form = null;
            public frmTextUseCommand Form
            {
                get
                {
                    return _Form;
                }
            }

            public string FormTitle
            {
                get
                {
                    return _Form.Text;
                }
            }

            public string AppPath
            {
                get
                {
                    return Application.StartupPath;
                }
            }

            private string _Name = "Zhang san";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            private DateTime _Birthday = new DateTime( 1990 , 1 , 1);

            public DateTime Birthday
            {
                get { return _Birthday; }
                set { _Birthday = value; }
            }

            private string _Nation = "China";

            public string Nation
            {
                get { return _Nation; }
                set { _Nation = value; }
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void menuOpenXMLDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenXMLDemo);
        }

        private void menuOpenRTFDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenRTFDemo);
        }

        private void mOpenFormViewDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenFormViewDemo);
        }

        /// <summary>
        /// Current file name.
        /// </summary>
        private string strFileName = null;
         
        /// <summary>
        /// Handle commands
        /// </summary>
        /// <param name="Command">command name</param>
        public void HandleCommand(string Command)
        {
            if (Command == null)
                return;
            Command = Command.Trim();
            try
            {
                switch (Command)
                {
                    case MyCommandNames.OpenXMLDemo:
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.demo.xml";
                            myEditControl.ExecuteCommand("FormViewMode", false, false);
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                myEditControl.LoadDocument(stream, FileFormat.XML);
                                stream.Close();
                            }
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.OpenFormViewDemo :
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.FormViewModeDemo.xml";
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                myEditControl.LoadDocument(stream, FileFormat.XML);
                                stream.Close();
                            }
                            myEditControl.ExecuteCommand("FormViewMode", false, true);
                            myEditControl.AutoScrollPosition = new Point(0, 0);
                            myEditControl.UpdateTextCaret();
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.OpenRTFDemo:
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.demo.rtf";
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                this.myEditControl.LoadDocument(stream, FileFormat.RTF);
                                stream.Close();
                            }
                            myEditControl.ExecuteCommand("FormViewMode", false, false);
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.New:
                        // new document
                        if (QuerySave())
                        {
                            this.myEditControl.ClearContent();
                            //myEditControl.Document.HeaderString = GetResourceString("_TEXTDEMO");
                            //myEditControl.Document.HeaderHeight = 100 ;
                            //myEditControl.Document.HeaderFont = new XFontValue( System.Windows.Forms.Control.DefaultFont.Name , 12 );
                            //myEditControl.Document.FooterString = "[%pageindex%]/[%pagecount%]";
                            //myEditControl.Document.FooterHeight = 100 ;
                            //myEditControl.Document.FooterFont = new XFontValue( System.Windows.Forms.Control.DefaultFont.Name , 12 );
                            myEditControl.RefreshDocument();
                            strFileName = null;
                        }
                        break;
                    case MyCommandNames.Open:
                        // open document
                        if (QuerySave())
                        {
                            using (System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog())
                            {
                                dlg.Filter = "*.xml|*.xml";
                                dlg.CheckFileExists = true;
                                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    this.Update();
                                    this.OpenDocument(dlg.FileName);
                                }
                            }
                        }
                        break;
                    case MyCommandNames.Save:
                        // save document
                        SaveDocument(strFileName);
                        break;
                    case MyCommandNames.SaveAs:
                        // save document as
                        SaveDocument(null);
                        break;
                      
                    //case MyCommandNames.ShowBookmark:
                    //    // show or hide bookmark
                    //    myEditControl.Document.ViewOptions.ShowBookmark = !myEditControl.Document.ViewOptions.ShowBookmark;
                    //    //menuShowBookmark.Checked = myEditControl.Document.ShowBookmark ;
                    //    this.mShowBookmark.Checked = myEditControl.Document.ViewOptions.ShowBookmark;
                    //    myEditControl.Invalidate();
                    //    break;
                    //case MyCommandNames.WordCount:
                    //    this.ShowWordCount();
                    //    break;
                     
                    default:
#if DEBUG
                        this.Alert("Bad command:" + Command);
#endif
                        break;
                }
            }
            catch (Exception ext)
            {
                this.Alert( "error:"  + ext.ToString());
                this.Status = "error:"  + ext.Message;
            }
            this.UpdateFormText();
        }


        /// <summary>
        /// Show a message box
        /// </summary>
        /// <param name="msg">message text</param>
        private void Alert(string msg)
        {
            MessageBox.Show(this, msg, this.Text);
        }


        /// <summary>
        /// get or set main status text
        /// </summary>
        private string Status
        {
            get { return this.lblStatus.Text; }
            set { this.lblStatus.Text = value; this.Update(); }
        }


        /// <summary>
        /// open special file
        /// </summary>
        /// <param name="fileName">file name ,it can be xml,rtf or txt file</param>
        private void OpenDocument(string fileName)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                string name = fileName.Trim().ToLower();
                FileFormat style = FileFormat.XML;
                if (name.EndsWith(".xml"))
                {
                    style = FileFormat.XML;
                }
                else if (name.EndsWith(".rtf"))
                {
                    style = FileFormat.RTF;
                }
                else if (name.EndsWith(".txt"))
                {
                    style = FileFormat.Text;
                }
                this.Status = "Loading" + fileName;
                this.myEditControl.LoadDocument(fileName, style);
                this.strFileName = fileName;
                this.Status = "loaded " + fileName;
                UpdateFormText();
            }
            catch (Exception ext)
            {
                this.Alert(
                    string.Format(
                        "open file'{0}' error:'{1}'",
                    fileName,
                    ext.ToString()));
                this.Status = string.Format(
                        "open file'{0}' error:'{1}'",
                    fileName,
                    ext.Message);
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// save document
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns></returns>
        private bool SaveDocument(string name)
        {
            if (name == null)
            {
                using (System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "*.xml|*.xml";
                    dlg.CheckPathExists = true;
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        name = dlg.FileName;
                    else
                        return false;
                }
                this.Update();
            }

            FileFormat style = FileFormat.XML;

            try
            {
                string name2 = name.Trim().ToLower();
                if (name2.EndsWith(".rtf"))
                    style = FileFormat.RTF;
                else
                    style = FileFormat.XML;
                this.Status = "Saving " + name;
                if (this.myEditControl.SaveDocument(name, style))
                {
                    strFileName = name;
                }
                this.Status = "Saved " + name;
                UpdateFormText();
                return true;
            }
            catch (Exception ext)
            {
                string txt = string.Format("Save file'{0}' error:'{1}'", name, ext.ToString());
                this.Alert(txt);
                this.Status = txt;
                return false;
            }
        }


        /// <summary>
        /// If document modified , show query message
        /// </summary>
        /// <returns>If return true , then can go no ; return false , cancel operation</returns>
        private bool QuerySave()
        {
            if (this.myEditControl.Document.Modified)
            {
                System.Windows.Forms.DialogResult result = MessageBox.Show(
                    this,
                    "File content changed ,save it?",
                    this.Text,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveDocument(strFileName) == false)
                        return false;
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                    return true;
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    return false;
            }
            return true;
        }
         
        /// <summary>
        /// handle element hover event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_HoverElementChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handle selection changed event in editor control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_SelectionChanged(object sender, EventArgs e)
        {
            

            DomContentLine line = myEditControl.Document.CurrentContentElement.CurrentLine;
            if (line != null && line.OwnerPage != null)
            {
                string txt =
                   string.Format("Page:{0} Line:{1} Column:{2}",
                        Convert.ToString(line.OwnerPage.PageIndex),
                        Convert.ToString(myEditControl.CurrentLineIndexInPage ),
                        Convert.ToString(myEditControl.CurrentColumnIndex));
                if (myEditControl.Selection.Length != 0)
                {
                    txt = txt + string.Format(
                        " Selected{0}elements",
                        Math.Abs(myEditControl.Selection.Length));
                }
                this.lblPosition.Text = txt;
            }
            UpdateFormText();
             
        }

        private void UpdateFormText()
        {
            string text = "DCSoft.CSharpWriter";
            if (string.IsNullOrEmpty(this.myEditControl.Document.Info.Title) == false)
            {
                text = myEditControl.Document.Info.Title + "-" + text;
            }
            else if ( string.IsNullOrEmpty( this.myEditControl.Document.FileName ) == false )
            {
                text =myEditControl.Document.FileName + " - " + text ;
            }
            if (myEditControl.Document.Modified)
            {
                text = text + " *";
            }
            this.Text = text;
        }

        private void frmTextUseCommand_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (QuerySave() == false)
            {
                e.Cancel = true;
            }
        }
         

        private void mTestInsertImage_Click(object sender, EventArgs e)
        {
            System.Drawing.Image img = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "About.jpg"));
            myEditControl.ExecuteCommand("InsertImage", false, img); return;
        }

        private void mTestInsertString_Click(object sender, EventArgs e)
        {
            myEditControl.ExecuteCommand("InsertString", false, "abc");
        }

        private void mTestInsertRTF_Click(object sender, EventArgs e)
        {
            myEditControl.ExecuteCommand("InsertRTF", false, @"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}
{\colortbl ;\red255\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}{\info{\horzdoc}{\*\lchars ([\'7b\'a1\'a4\'a1\'ae\'a1\'b0\'a1\'b4\'a1\'b6\'a1\'b8\'a1\'ba\'a1\'be\'a1\'b2\'a1\'bc\'a3\'a8\'a3\'ae\'a3\'db\'a3\'fb\'a1\'ea\'a3\'a4}{\*\fchars !),.:\'3b?]\'7d\'a1\'a7\'a1\'a4\'a1\'a6\'a1\'a5\'a8\'44\'a1\'ac\'a1\'af\'a1\'b1\'a1\'ad\'a1\'c3\'a1\'a2\'a1\'a3\'a1\'a8\'a1\'a9\'a1\'b5\'a1\'b7\'a1\'b9\'a1\'bb\'a1\'bf\'a1\'b3\'a1\'bd\'a3\'a1\'a3\'a2\'a3\'a7\'a3\'a9\'a3\'ac\'a3\'ae\'a3\'ba\'a3\'bb\'a3\'bf\'a3\'dd\'a3\'e0\'a3\'fc\'a3\'fd\'a1\'ab\'a1\'e9}}
\viewkind4\uc1\pard\sa200\sl276\slmult1\lang2052\f0\fs22 1\cf1 2\cf0 3\par
}");
        }

        private void mTestInsertXML_Click(object sender, EventArgs e)
        {

            myEditControl.XMLText = @"<?xml version='1.0'?>
<XTextDocument xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
  <XElements>
    <Element xsi:type='XTextBody'>
      <XElements>
        <Element xsi:type='XString'>
          <Text>111</Text>
        </Element>
        <Element xsi:type='XString' StyleIndex='0'>
          <Text>111</Text>
        </Element>
        <Element xsi:type='XString' StyleIndex='1'>
          <Text>11</Text>
        </Element>
        <Element xsi:type='XString' StyleIndex='2'>
          <Text>1</Text>
        </Element>
        <Element xsi:type='XString'>
          <Text>11</Text>
        </Element>
        <Element xsi:type='XParagraphFlag' />
      </XElements>
    </Element>
    <Element xsi:type='XTextHeader'>
      <XElements>
        <Element xsi:type='XParagraphFlag' />
      </XElements>
    </Element>
    <Element xsi:type='XTextFooter'>
      <XElements>
        <Element xsi:type='XParagraphFlag' />
      </XElements>
    </Element>
  </XElements>
  <Info>
    <CreationTime>2012-03-29T15:47:51.1032576+08:00</CreationTime>
    <LastModifiedTime>2012-03-29T15:47:51.1042577+08:00</LastModifiedTime>
    <LastPrintTime>1980-01-01T00:00:00</LastPrintTime>
    <Operator>DCSoft.CSharpWriter Version:1.0.1111.28434</Operator>
  </Info>
  <DefaultFont>
    <Size>12</Size>
  </DefaultFont>
  <ContentStyles>
    <Default>
      <FontName>宋体</FontName>
      <FontSize>12</FontSize>
    </Default>
    <Styles>
      <Style Index='0'>
        <Bold>true</Bold>
      </Style>
      <Style Index='1'>
        <FontSize>24</FontSize>
        <Bold>true</Bold>
      </Style>
      <Style Index='2'>
        <FontSize>24</FontSize>
      </Style>
    </Styles>
  </ContentStyles>
  <DocumentGraphicsUnit>Document</DocumentGraphicsUnit>
  <PageSettings>
    <DesignerPaperWidth>0</DesignerPaperWidth>
    <DesignerPaperHeight>0</DesignerPaperHeight>
  </PageSettings>
  <CustomerParameters />
</XTextDocument>";
        }

        private void mTestUpdateData_Click(object sender, EventArgs e)
        { 
        }

        private void mXML2RTF_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "Conver a xml to rtf file" );
            string sourceFileName = null;
            string descFileName = null;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "*.xml|*.xml";
                dlg.CheckFileExists = true;
                dlg.ShowReadOnly = false;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    sourceFileName = dlg.FileName;
                }
                else
                {
                    return;
                }
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "RTF文件(*.rtf)|*.rtf";
                dlg.CheckPathExists = true;
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    descFileName = dlg.FileName;
                }
                else
                {
                    return;
                }
            }

            DomDocument document = new DomDocument();
            document.Load(sourceFileName, FileFormat.XML);
            using (System.Drawing.Graphics g = document.CreateGraphics())
            {
                document.RefreshSize(g);
                document.ExecuteLayout();
                document.RefreshPages();
                document.Save(descFileName, FileFormat.RTF);
            }
            MessageBox.Show( string.Format( "success convert xml file'{0}' to rtf file'{1}'" , sourceFileName , descFileName ));
        }

        /// <summary>
        /// 文档内容发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_DocumentContentChanged(object sender, EventArgs e)
        {
            //System.Console.WriteLine("");
            //System.Diagnostics.Debug.WriteLine(
            //    System.Environment.TickCount + ":" + myEditControl.DocumentContentVersion);
            //XTextInputFieldElement field = myEditControl.Document.CurrentField as XTextInputFieldElement;

        }
         
        private void mTestInsertCheckBoxList_Click(object sender, EventArgs e)
        {
            myEditControl.ExecuteCommand("InsertCheckBoxList", true, null);

            //myEditControl.Document.GetSpecifyElements(typeof(XTextCheckBoxElement));
        }
         
        private void myEditControl_StatusTextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = myEditControl.StatusText;
            this.statusStrip1.Refresh();
        }
         
        /// <summary>
        /// 编辑器中执行命令时的错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void myEditControl_CommandError(object sender, CommandErrorEventArgs args)
        {
            MessageBox.Show(
                this,
                args.CommandName + "\r\n" + args.Exception.ToString(), 
                this.Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);

        }

        private void mLocalFileSystem_Click(object sender, EventArgs e)
        {

        }
    }
}