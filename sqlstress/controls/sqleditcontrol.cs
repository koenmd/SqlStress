using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;

namespace sqlstress
{
    public class sqleditcontrol
    {
        private ScintillaNET.Scintilla TextArea;
        public sqleditcontrol(ScintillaNET.Scintilla editor)
        {
            // CREATE CONTROL
            TextArea = editor;
            //TextPanel.Controls.Add(TextArea);

            // BASIC CONFIG
            //TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            //TextArea.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.Word;
            //TextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();

            InitSyntaxColoring();

            // NUMBER MARGIN
            //InitNumberMargin();

            // BOOKMARK MARGIN
            //InitBookmarkMargin();

            // CODE FOLDING MARGIN
            //InitCodeFolding();

            // DRAG DROP
            //InitDragDropFile();

            // DEFAULT FILE
            //LoadDataFromFile("../../MainForm.cs");

            // INIT HOTKEYS
            InitHotkeys();

        }
        private void InitColors()
        {
            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        private void InitHotkeys()
        {
            /*
            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(this, OpenFindDialog, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.R, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.H, true);
            HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);            
            HotKeyManager.AddHotKey(this, CloseSearch, Keys.Escape);
            */

            // remove conflicting hotkeys from scintilla
            TextArea.ClearCmdKey(Keys.Control | Keys.F);
            TextArea.ClearCmdKey(Keys.Control | Keys.R);
            TextArea.ClearCmdKey(Keys.Control | Keys.H);
            TextArea.ClearCmdKey(Keys.Control | Keys.L);
            TextArea.ClearCmdKey(Keys.Control | Keys.U);

        }

        private void InitSyntaxColoring()
        {
            
            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 10;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            /*
            // Configure the CPP (C#) lexer styles
            TextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);
            
            TextArea.Lexer = Lexer.Cpp;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");
            */

            TextArea.Styles[Style.Sql.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Sql.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Sql.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Sql.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Sql.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.Character].ForeColor = IntToColor(0xE95454);
            //TextArea.Styles[Style.Sql.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Sql.Operator].ForeColor = IntToColor(0xE0E0E0);
            //TextArea.Styles[Style.Sql.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Sql.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Sql.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Sql.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Sql.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Sql.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            //TextArea.Styles[Style.Sql.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            TextArea.Lexer = Lexer.Sql;

            TextArea.SetKeywords(0, "go create begin end select insert update delete primary case do while else if in throw set var try catch while with default break continue return use as exception else from goto group order by into where top values");
            TextArea.SetKeywords(1, "table view index procedure proc symbol constraint function true false null char bool int numeric float double date image text");
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        internal class HotKeyManager
        {

            public static bool Enable = true;

            public static void AddHotKey(Form form, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
            {
                form.KeyPreview = true;

                form.KeyDown += delegate (object sender, KeyEventArgs e)
                {
                    if (IsHotkey(e, key, ctrl, shift, alt))
                    {
                        function();
                    }
                };
            }

            public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
            {
                return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
            }

        }

    }
}
