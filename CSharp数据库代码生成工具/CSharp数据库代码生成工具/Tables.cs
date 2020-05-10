using System.Collections;
using System.IO;
using System.Xml;
using Common;
using CSharp数据库代码生成工具.Properties;
using Maunite.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Dapper;

namespace CSharp数据库代码生成工具
{
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }
        private string _strConn = "";
        public string StrDatabase,StrTableName="";

        bool IsMySql = false;

        private static MySqlConnection connection;
        public static MySqlConnection MySqlConnection
        {
            get
            {
                return connection;
            }
        }

        private MySqlConnection GetConnection(string  mysqlconnectionString) 
        {
            MySqlConnection con = new MySqlConnection(mysqlconnectionString);

            return con;
        }


        public Dictionary<string, string> dic = new Dictionary<string, string>();
        public string strTableName
        {
            get
            {
                return StrTableName;
            }
        }
        public string strDatabase
        {
            get
            {
                return StrDatabase;
            }
        }
        
        private void Tables_Load(object sender, EventArgs e)
        {
            //Hide();
            listViewTables.GridLines = true;//显示各个记录的分隔线 
            listViewTables.FullRowSelect = true;//要选择就是一行 
            listViewTables.View = View.Details;//定义列表显示的方式 
            listViewTables.Scrollable = true;//需要时候显示滚动条 
            listViewTables.MultiSelect = false; // 不可以多行选择 
            listViewTables.HeaderStyle = ColumnHeaderStyle.Clickable;

           // tabPage2.Parent = tabPage1.Parent = null;

            labDataBase.Visible = false;
            comDataBase.Visible = false;

            label4.Text = @"如果在使用中有不明白的地方可以咨询作者。";
            label5.Text = @"作者:高增智";
            label6.Text = @"QQ:365238004 ";
            //linkLabel1.lin

        // StrDatabase = ((form11)this.Owner).comDataBaseText;
            // LoadData();
            
            //http://sandbox.runjs.cn/show/nhuozgst
            webBrowser1.DocumentText =GetHtml();
        }

        #region HTML Json生成c#类
        public string GetHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("	");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>json生成c#类</title>");
            sb.AppendLine("<script type=\"text/javascript\"> " + GetJavascript() + " </script>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("	");
            sb.AppendLine("	<h1>json生成C#类小工具</h1>");
            sb.AppendLine("	<h5>JSON 字符串</h5>");
            sb.AppendLine("	<div>");
            sb.AppendLine("		<textarea style=\"width:600px;height:300px;margin-bottom:5px;\" id=\"jsonStr\">{");
            sb.AppendLine("    ");
            sb.AppendLine("	name:\"用户名\",password:\"密码\"");
            sb.AppendLine("");
            sb.AppendLine("}</textarea>");
            sb.AppendLine("		<br>");
            sb.AppendLine("		<button onclick=\"document.getElementById('jsonStr').value='';document.getElementById('class').innerHTML=''\">清除</button>");
            sb.AppendLine("		<button onclick=\"do_js_beautify()\">格式化代码</button>");
            sb.AppendLine("		<button onclick=\"startGen()\">生成C#类</button>");
            sb.AppendLine("	</div>");
            sb.AppendLine("");
            sb.AppendLine("	<h5>C#类代码&nbsp;<button onclick=\"selectCode()\">选中代码</button></h5>");
            sb.AppendLine("	");
            sb.AppendLine("	<pre class=\"prettyprint\" id=\"class\" style=\"border:1px solid #ccc; padding:10px; width:800px;\"> ");
            sb.AppendLine("			");
            sb.AppendLine("	</pre>");
            sb.AppendLine("	");
            sb.AppendLine("	<script>");
            sb.AppendLine("	");
            sb.AppendLine("		String.prototype.format = function(){");
            sb.AppendLine("			var args = arguments;");
            sb.AppendLine("			return this.replace(/\\{(\\d+)\\}/g,                ");
            sb.AppendLine("				function(m,i){");
            sb.AppendLine("					return args[i];");
            sb.AppendLine("			});");
            sb.AppendLine("		}");
            sb.AppendLine("		");
            sb.AppendLine("		String.prototype.trim=function(){");
            sb.AppendLine("			 return this.replace(/(^\\s*)|(\\s*$)/g,\"\");");
            sb.AppendLine("		}");
            sb.AppendLine("		");
            sb.AppendLine("		JSON2CSharp={");
            sb.AppendLine("			_allClass:[],");
            sb.AppendLine("			_genClassCode:function(obj,name){");
            sb.AppendLine("				var clas=\"public class {0}\\r\\n{\\r\\n\".format(name || \"Root\");");
            sb.AppendLine("				for(var n in obj){");
            sb.AppendLine("					var v = obj[n];");
            sb.AppendLine("					n = n.trim();");
            sb.AppendLine("					clas += \"    {0}    public {1} {2} { get; set; }\\r\\n\\r\\n\".format(this._genComment(v),this._genTypeByProp(n,v),n);");
            sb.AppendLine("				}");
            sb.AppendLine("				clas += \"}\\r\\n\\r\\n\";");
            sb.AppendLine("				this._allClass.push(clas);");
            sb.AppendLine("				return this._allClass.join(\"\\r\\n\\r\\n\");");
            sb.AppendLine("			},");
            sb.AppendLine("			_genTypeByProp:function(name,val){");
            sb.AppendLine("				switch(Object.prototype.toString.apply(val)){");
            sb.AppendLine("					case \"[object Number]\" :{");
            sb.AppendLine("						return val.toString().indexOf(\".\") > -1 ? \"double\" : \"int\";");
            sb.AppendLine("					}");
            sb.AppendLine("					case \"[object Date]\":{");
            sb.AppendLine("						return \"DateTime\";");
            sb.AppendLine("					}");
            sb.AppendLine("					case \"[object Object]\":{");
            sb.AppendLine("						this._genClassCode(val,name);");
            sb.AppendLine("						return name;");
            sb.AppendLine("					}");
            sb.AppendLine("					case \"[object Array]\":{");
            sb.AppendLine("						return \"List&#60;{0}&#62;\".format(this._genTypeByProp(name+\"Item\",val[0]));");
            sb.AppendLine("					}");
            sb.AppendLine("					default:{");
            sb.AppendLine("						return \"string\";");
            sb.AppendLine("					}");
            sb.AppendLine("				}	");
            sb.AppendLine("			},");
            sb.AppendLine("			_genComment:function(val){");
            sb.AppendLine("				var commm= typeof(val) == \"string\" && /.*[\\u4e00-\\u9fa5]+.*$/.test(val) ? val : \"\" ;");
            sb.AppendLine("				return \"\"; //\"/// &#60;summary&#62;\\r\\n    /// \"+commm+ \"\\r\\n    /// &#60;/summary&#62;\\r\\n\";");
            sb.AppendLine("			},");
            sb.AppendLine("			convert:function(jsonObj){");
            sb.AppendLine("				this._allClass=[];");
            sb.AppendLine("				return this._genClassCode(jsonObj);");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine("		");
            sb.AppendLine("		");
            sb.AppendLine("		");
            sb.AppendLine("		function do_js_beautify() {");
            sb.AppendLine("			var js_source =document.getElementById(\"jsonStr\").value.replace(/^\\s+/, '');");
            sb.AppendLine("			if(js_source.length==0)");
            sb.AppendLine("				return;");
            sb.AppendLine("			tabchar = ' ';");
            sb.AppendLine("			var fjs = js_beautify(js_source);");
            sb.AppendLine("			document.getElementById(\"jsonStr\").value=fjs;");
            sb.AppendLine("		}");
            sb.AppendLine("");
            sb.AppendLine("		");
            sb.AppendLine("		function startGen(){");
            sb.AppendLine("			try{");
            sb.AppendLine("				var v = eval(\"(\"+document.getElementById(\"jsonStr\").value+\")\");");
            sb.AppendLine("				document.getElementById(\"class\").className =\"prettyprint\";");
            sb.AppendLine("				document.getElementById(\"class\").innerHTML=JSON2CSharp.convert(v);");
            sb.AppendLine("				document.getElementById(\"jsonStr\").focus();");
            sb.AppendLine("			}catch(e){");
            sb.AppendLine("				alert(e.message);");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine("		");
            sb.AppendLine("		function selectCode() {");
            sb.AppendLine("			if (document.selection) {");
            sb.AppendLine("				var range = document.body.createTextRange();");
            sb.AppendLine("				range.moveToElementText(document.getElementById('class'));");
            sb.AppendLine("				range.select();");
            sb.AppendLine("			} else if (window.getSelection) {");
            sb.AppendLine("				var range = document.createRange();");
            sb.AppendLine("				range.selectNode(document.getElementById('class'));");
            sb.AppendLine("				window.getSelection().addRange(range);");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine("	</script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        } 
        #endregion

        #region Javascript 生成c#类
        public string GetJavascript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("/*");
            sb.AppendLine("");
            sb.AppendLine(" JS Beautifier");
            sb.AppendLine("---------------");
            sb.AppendLine("  $Date: 2008-06-10 14:49:11 +0300 (Tue, 10 Jun 2008) $");
            sb.AppendLine("  $Revision: 60 $");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("  Written by Einars \"elfz\" Lielmanis, <elfz@laacz.lv> ");
            sb.AppendLine("      http://elfz.laacz.lv/beautify/");
            sb.AppendLine("");
            sb.AppendLine("  Originally converted to javascript by Vital, <vital76@gmail.com> ");
            sb.AppendLine("      http://my.opera.com/Vital/blog/2007/11/21/javascript-beautify-on-javascript-translated");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("  You are free to use this in any way you want, in case you find this useful or working for you.");
            sb.AppendLine("");
            sb.AppendLine("  Usage:");
            sb.AppendLine("    js_beautify(js_source_text);");
            sb.AppendLine("");
            sb.AppendLine("*/");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("function js_beautify(js_source_text, indent_size, indent_character, indent_level)");
            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine("    var input, output, token_text, last_type, last_text, last_word, current_mode, modes, indent_string;");
            sb.AppendLine("    var whitespace, wordchar, punct, parser_pos, line_starters, in_case;");
            sb.AppendLine("    var prefix, token_type, do_block_just_closed, var_line, var_line_tainted;");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function trim_output()");
            sb.AppendLine("    {");
            sb.AppendLine("        while (output.length && (output[output.length - 1] === ' ' || output[output.length - 1] === indent_string)) {");
            sb.AppendLine("            output.pop();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    function print_newline(ignore_repeated)");
            sb.AppendLine("    {");
            sb.AppendLine("        ignore_repeated = typeof ignore_repeated === 'undefined' ? true: ignore_repeated;");
            sb.AppendLine("        ");
            sb.AppendLine("        trim_output();");
            sb.AppendLine("");
            sb.AppendLine("        if (!output.length) {");
            sb.AppendLine("            return; // no newline on start of file");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (output[output.length - 1] !== \"\\n\" || !ignore_repeated) {");
            sb.AppendLine("            output.push(\"\\n\");");
            sb.AppendLine("        }");
            sb.AppendLine("        for (var i = 0; i < indent_level; i++) {");
            sb.AppendLine("            output.push(indent_string);");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function print_space()");
            sb.AppendLine("    {");
            sb.AppendLine("        var last_output = output.length ? output[output.length - 1] : ' ';");
            sb.AppendLine("        if (last_output !== ' ' && last_output !== '\\n' && last_output !== indent_string) { // prevent occassional duplicate space");
            sb.AppendLine("            output.push(' ');");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function print_token()");
            sb.AppendLine("    {");
            sb.AppendLine("        output.push(token_text);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    function indent()");
            sb.AppendLine("    {");
            sb.AppendLine("        indent_level++;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function unindent()");
            sb.AppendLine("    {");
            sb.AppendLine("        if (indent_level) {");
            sb.AppendLine("            indent_level--;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function remove_indent()");
            sb.AppendLine("    {");
            sb.AppendLine("        if (output.length && output[output.length - 1] === indent_string) {");
            sb.AppendLine("            output.pop();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function set_mode(mode)");
            sb.AppendLine("    {");
            sb.AppendLine("        modes.push(current_mode);");
            sb.AppendLine("        current_mode = mode;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function restore_mode()");
            sb.AppendLine("    {");
            sb.AppendLine("        do_block_just_closed = current_mode === 'DO_BLOCK';");
            sb.AppendLine("        current_mode = modes.pop();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function in_array(what, arr)");
            sb.AppendLine("    {");
            sb.AppendLine("        for (var i = 0; i < arr.length; i++)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (arr[i] === what) {");
            sb.AppendLine("                return true;");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("        return false;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    function get_next_token()");
            sb.AppendLine("    {");
            sb.AppendLine("        var n_newlines = 0;");
            sb.AppendLine("        var c = '';");
            sb.AppendLine("");
            sb.AppendLine("        do {");
            sb.AppendLine("            if (parser_pos >= input.length) {");
            sb.AppendLine("                return ['', 'TK_EOF'];");
            sb.AppendLine("            }");
            sb.AppendLine("            c = input.charAt(parser_pos);");
            sb.AppendLine("");
            sb.AppendLine("            parser_pos += 1;");
            sb.AppendLine("            if (c === \"\\n\") {");
            sb.AppendLine("                n_newlines += 1;");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("        while (in_array(c, whitespace));");
            sb.AppendLine("");
            sb.AppendLine("        if (n_newlines > 1) {");
            sb.AppendLine("            for (var i = 0; i < 2; i++) {");
            sb.AppendLine("                print_newline(i === 0);");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("        var wanted_newline = (n_newlines === 1);");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("        if (in_array(c, wordchar)) {");
            sb.AppendLine("            if (parser_pos < input.length) {");
            sb.AppendLine("                while (in_array(input.charAt(parser_pos), wordchar)) {");
            sb.AppendLine("                    c += input.charAt(parser_pos);");
            sb.AppendLine("                    parser_pos += 1;");
            sb.AppendLine("                    if (parser_pos === input.length) {");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            // small and surprisingly unugly hack for 1E-10 representation");
            sb.AppendLine("            if (parser_pos !== input.length && c.match(/^[0-9]+[Ee]$/) && input.charAt(parser_pos) === '-') {");
            sb.AppendLine("                parser_pos += 1;");
            sb.AppendLine("");
            sb.AppendLine("                var t = get_next_token(parser_pos);");
            sb.AppendLine("                c += '-' + t[0];");
            sb.AppendLine("                return [c, 'TK_WORD'];");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            if (c === 'in') { // hack for 'in' operator");
            sb.AppendLine("                return [c, 'TK_OPERATOR'];");
            sb.AppendLine("            }");
            sb.AppendLine("            return [c, 'TK_WORD'];");
            sb.AppendLine("        }");
            sb.AppendLine("        ");
            sb.AppendLine("        if (c === '(' || c === '[') {");
            sb.AppendLine("            return [c, 'TK_START_EXPR'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === ')' || c === ']') {");
            sb.AppendLine("            return [c, 'TK_END_EXPR'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === '{') {");
            sb.AppendLine("            return [c, 'TK_START_BLOCK'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === '}') {");
            sb.AppendLine("            return [c, 'TK_END_BLOCK'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === ';') {");
            sb.AppendLine("            return [c, 'TK_END_COMMAND'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === '/') {");
            sb.AppendLine("            var comment = '';");
            sb.AppendLine("            // peek for comment /* ... */");
            sb.AppendLine("            if (input.charAt(parser_pos) === '*') {");
            sb.AppendLine("                parser_pos += 1;");
            sb.AppendLine("                if (parser_pos < input.length) {");
            sb.AppendLine("                    while (! (input.charAt(parser_pos) === '*' && input.charAt(parser_pos + 1) && input.charAt(parser_pos + 1) === '/') && parser_pos < input.length) {");
            sb.AppendLine("                        comment += input.charAt(parser_pos);");
            sb.AppendLine("                        parser_pos += 1;");
            sb.AppendLine("                        if (parser_pos >= input.length) {");
            sb.AppendLine("                            break;");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("                parser_pos += 2;");
            sb.AppendLine("                return ['/*' + comment + '*/', 'TK_BLOCK_COMMENT'];");
            sb.AppendLine("            }");
            sb.AppendLine("            // peek for comment // ...");
            sb.AppendLine("            if (input.charAt(parser_pos) === '/') {");
            sb.AppendLine("                comment = c;");
            sb.AppendLine("                while (input.charAt(parser_pos) !== \"\\x0d\" && input.charAt(parser_pos) !== \"\\x0a\") {");
            sb.AppendLine("                    comment += input.charAt(parser_pos);");
            sb.AppendLine("                    parser_pos += 1;");
            sb.AppendLine("                    if (parser_pos >= input.length) {");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("                parser_pos += 1;");
            sb.AppendLine("                if (wanted_newline) {");
            sb.AppendLine("                    print_newline();");
            sb.AppendLine("                }");
            sb.AppendLine("                return [comment, 'TK_COMMENT'];");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (c === \"'\" || // string");
            sb.AppendLine("        c === '\"' || // string");
            sb.AppendLine("        (c === '/' &&");
            sb.AppendLine("        ((last_type === 'TK_WORD' && last_text === 'return') || (last_type === 'TK_START_EXPR' || last_type === 'TK_END_BLOCK' || last_type === 'TK_OPERATOR' || last_type === 'TK_EOF' || last_type === 'TK_END_COMMAND')))) { // regexp");
            sb.AppendLine("            var sep = c;");
            sb.AppendLine("            var esc = false;");
            sb.AppendLine("            c = '';");
            sb.AppendLine("");
            sb.AppendLine("            if (parser_pos < input.length) {");
            sb.AppendLine("");
            sb.AppendLine("                while (esc || input.charAt(parser_pos) !== sep) {");
            sb.AppendLine("                    c += input.charAt(parser_pos);");
            sb.AppendLine("                    if (!esc) {");
            sb.AppendLine("                        esc = input.charAt(parser_pos) === '\\\\';");
            sb.AppendLine("                    } else {");
            sb.AppendLine("                        esc = false;");
            sb.AppendLine("                    }");
            sb.AppendLine("                    parser_pos += 1;");
            sb.AppendLine("                    if (parser_pos >= input.length) {");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            parser_pos += 1;");
            sb.AppendLine("            if (last_type === 'TK_END_COMMAND') {");
            sb.AppendLine("                print_newline();");
            sb.AppendLine("            }");
            sb.AppendLine("            return [sep + c + sep, 'TK_STRING'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        if (in_array(c, punct)) {");
            sb.AppendLine("            while (parser_pos < input.length && in_array(c + input.charAt(parser_pos), punct)) {");
            sb.AppendLine("                c += input.charAt(parser_pos);");
            sb.AppendLine("                parser_pos += 1;");
            sb.AppendLine("                if (parser_pos >= input.length) {");
            sb.AppendLine("                    break;");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            return [c, 'TK_OPERATOR'];");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        return [c, 'TK_UNKNOWN'];");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("    //----------------------------------");
            sb.AppendLine("");
            sb.AppendLine("    indent_character = indent_character || ' ';");
            sb.AppendLine("    indent_size = indent_size || 4;");
            sb.AppendLine("");
            sb.AppendLine("    indent_string = '';");
            sb.AppendLine("    while (indent_size--) {");
            sb.AppendLine("        indent_string += indent_character;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    input = js_source_text;");
            sb.AppendLine("");
            sb.AppendLine("    last_word = ''; // last 'TK_WORD' passed");
            sb.AppendLine("    last_type = 'TK_START_EXPR'; // last token type");
            sb.AppendLine("    last_text = ''; // last token text");
            sb.AppendLine("    output = [];");
            sb.AppendLine("");
            sb.AppendLine("    do_block_just_closed = false;");
            sb.AppendLine("    var_line = false;");
            sb.AppendLine("    var_line_tainted = false;");
            sb.AppendLine("");
            sb.AppendLine("    whitespace = \"\\n\\r\\t \".split('');");
            sb.AppendLine("    wordchar = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_$'.split('');");
            sb.AppendLine("    punct = '+ - * / % & ++ -- = += -= *= /= %= == === != !== > < >= <= >> << >>> >>>= >>= <<= && &= | || ! !! , : ? ^ ^= |='.split(' ');");
            sb.AppendLine("");
            sb.AppendLine("    // words which should always start on new line.");
            sb.AppendLine("    line_starters = 'continue,try,throw,return,var,if,switch,case,default,for,while,break,function'.split(',');");
            sb.AppendLine("");
            sb.AppendLine("    // states showing if we are currently in expression (i.e. \"if\" case) - 'EXPRESSION', or in usual block (like, procedure), 'BLOCK'.");
            sb.AppendLine("    // some formatting depends on that.");
            sb.AppendLine("    current_mode = 'BLOCK';");
            sb.AppendLine("    modes = [current_mode];");
            sb.AppendLine("");
            sb.AppendLine("    indent_level = indent_level || 0;");
            sb.AppendLine("    parser_pos = 0; // parser position");
            sb.AppendLine("    in_case = false; // flag for parser that case/default has been processed, and next colon needs special attention");
            sb.AppendLine("    while (true) {");
            sb.AppendLine("        var t = get_next_token(parser_pos);");
            sb.AppendLine("        token_text = t[0];");
            sb.AppendLine("        token_type = t[1];");
            sb.AppendLine("        if (token_type === 'TK_EOF') {");
            sb.AppendLine("            break;");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        switch (token_type) {");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_START_EXPR':");
            sb.AppendLine("            var_line = false;");
            sb.AppendLine("            set_mode('EXPRESSION');");
            sb.AppendLine("            if (last_type === 'TK_END_EXPR' || last_type === 'TK_START_EXPR') {");
            sb.AppendLine("                // do nothing on (( and )( and ][ and ]( ..");
            sb.AppendLine("            } else if (last_type !== 'TK_WORD' && last_type !== 'TK_OPERATOR') {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            } else if (in_array(last_word, line_starters) && last_word !== 'function') {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            }");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_END_EXPR':");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            restore_mode();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_START_BLOCK':");
            sb.AppendLine("            ");
            sb.AppendLine("            if (last_word === 'do') {");
            sb.AppendLine("                set_mode('DO_BLOCK');");
            sb.AppendLine("            } else {");
            sb.AppendLine("                set_mode('BLOCK');");
            sb.AppendLine("            }");
            sb.AppendLine("            if (last_type !== 'TK_OPERATOR' && last_type !== 'TK_START_EXPR') {");
            sb.AppendLine("                if (last_type === 'TK_START_BLOCK') {");
            sb.AppendLine("                    print_newline();");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    print_space();");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            indent();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_END_BLOCK':");
            sb.AppendLine("            if (last_type === 'TK_START_BLOCK') {");
            sb.AppendLine("                // nothing");
            sb.AppendLine("                trim_output();");
            sb.AppendLine("                unindent();");
            sb.AppendLine("            } else {");
            sb.AppendLine("                unindent();");
            sb.AppendLine("                print_newline();");
            sb.AppendLine("            }");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            restore_mode();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_WORD':");
            sb.AppendLine("");
            sb.AppendLine("            if (do_block_just_closed) {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("                print_token();");
            sb.AppendLine("                print_space();");
            sb.AppendLine("                break;");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            if (token_text === 'case' || token_text === 'default') {");
            sb.AppendLine("                if (last_text === ':') {");
            sb.AppendLine("                    // switch cases following one another");
            sb.AppendLine("                    remove_indent();");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    // case statement starts in the same line where switch");
            sb.AppendLine("                    unindent();");
            sb.AppendLine("                    print_newline();");
            sb.AppendLine("                    indent();");
            sb.AppendLine("                }");
            sb.AppendLine("                print_token();");
            sb.AppendLine("                in_case = true;");
            sb.AppendLine("                break;");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("            prefix = 'NONE';");
            sb.AppendLine("            if (last_type === 'TK_END_BLOCK') {");
            sb.AppendLine("                if (!in_array(token_text.toLowerCase(), ['else', 'catch', 'finally'])) {");
            sb.AppendLine("                    prefix = 'NEWLINE';");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    prefix = 'SPACE';");
            sb.AppendLine("                    print_space();");
            sb.AppendLine("                }");
            sb.AppendLine("            } else if (last_type === 'TK_END_COMMAND' && (current_mode === 'BLOCK' || current_mode === 'DO_BLOCK')) {");
            sb.AppendLine("                prefix = 'NEWLINE';");
            sb.AppendLine("            } else if (last_type === 'TK_END_COMMAND' && current_mode === 'EXPRESSION') {");
            sb.AppendLine("                prefix = 'SPACE';");
            sb.AppendLine("            } else if (last_type === 'TK_WORD') {");
            sb.AppendLine("                prefix = 'SPACE';");
            sb.AppendLine("            } else if (last_type === 'TK_START_BLOCK') {");
            sb.AppendLine("                prefix = 'NEWLINE';");
            sb.AppendLine("            } else if (last_type === 'TK_END_EXPR') {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("                prefix = 'NEWLINE';");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            if (last_type !== 'TK_END_BLOCK' && in_array(token_text.toLowerCase(), ['else', 'catch', 'finally'])) {");
            sb.AppendLine("                print_newline();");
            sb.AppendLine("            } else if (in_array(token_text, line_starters) || prefix === 'NEWLINE') {");
            sb.AppendLine("                if (last_text === 'else') {");
            sb.AppendLine("                    // no need to force newline on else break");
            sb.AppendLine("                    print_space();");
            sb.AppendLine("                } else if ((last_type === 'TK_START_EXPR' || last_text === '=') && token_text === 'function') {");
            sb.AppendLine("                    // no need to force newline on 'function': (function");
            sb.AppendLine("                    // DONOTHING");
            sb.AppendLine("                } else if (last_type === 'TK_WORD' && (last_text === 'return' || last_text === 'throw')) {");
            sb.AppendLine("                    // no newline between 'return nnn'");
            sb.AppendLine("                    print_space();");
            sb.AppendLine("                } else if (last_type !== 'TK_END_EXPR') {");
            sb.AppendLine("                    if ((last_type !== 'TK_START_EXPR' || token_text !== 'var') && last_text !== ':') {");
            sb.AppendLine("                        // no need to force newline on 'var': for (var x = 0...)");
            sb.AppendLine("                        if (token_text === 'if' && last_type === 'TK_WORD' && last_word === 'else') {");
            sb.AppendLine("                            // no newline for } else if {");
            sb.AppendLine("                            print_space();");
            sb.AppendLine("                        } else {");
            sb.AppendLine("                            print_newline();");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    if (in_array(token_text, line_starters) && last_text !== ')') {");
            sb.AppendLine("                        print_newline();");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            } else if (prefix === 'SPACE') {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            }");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            last_word = token_text;");
            sb.AppendLine("");
            sb.AppendLine("            if (token_text === 'var') {");
            sb.AppendLine("                var_line = true;");
            sb.AppendLine("                var_line_tainted = false;");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_END_COMMAND':");
            sb.AppendLine("");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            var_line = false;");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_STRING':");
            sb.AppendLine("");
            sb.AppendLine("            if (last_type === 'TK_START_BLOCK' || last_type === 'TK_END_BLOCK') {");
            sb.AppendLine("                print_newline();");
            sb.AppendLine("            } else if (last_type === 'TK_WORD') {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            }");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_OPERATOR':");
            sb.AppendLine("");
            sb.AppendLine("            var start_delim = true;");
            sb.AppendLine("            var end_delim = true;");
            sb.AppendLine("            if (var_line && token_text !== ',') {");
            sb.AppendLine("                var_line_tainted = true;");
            sb.AppendLine("                if (token_text === ':') {");
            sb.AppendLine("                    var_line = false;");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            if (token_text === ':' && in_case) {");
            sb.AppendLine("                print_token(); // colon really asks for separate treatment");
            sb.AppendLine("                print_newline();");
            sb.AppendLine("                break;");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            in_case = false;");
            sb.AppendLine("");
            sb.AppendLine("            if (token_text === ',') {");
            sb.AppendLine("                if (var_line) {");
            sb.AppendLine("                    if (var_line_tainted) {");
            sb.AppendLine("                        print_token();");
            sb.AppendLine("                        print_newline();");
            sb.AppendLine("                        var_line_tainted = false;");
            sb.AppendLine("                    } else {");
            sb.AppendLine("                        print_token();");
            sb.AppendLine("                        print_space();");
            sb.AppendLine("                    }");
            sb.AppendLine("                } else if (last_type === 'TK_END_BLOCK') {");
            sb.AppendLine("                    print_token();");
            sb.AppendLine("                    print_newline();");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    if (current_mode === 'BLOCK') {");
            sb.AppendLine("                        print_token();");
            sb.AppendLine("                        print_newline();");
            sb.AppendLine("                    } else {");
            sb.AppendLine("                        // EXPR od DO_BLOCK");
            sb.AppendLine("                        print_token();");
            sb.AppendLine("                        print_space();");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("                break;");
            sb.AppendLine("            } else if (token_text === '--' || token_text === '++') { // unary operators special case");
            sb.AppendLine("                if (last_text === ';') {");
            sb.AppendLine("                    // space for (;; ++i)");
            sb.AppendLine("                    start_delim = true;");
            sb.AppendLine("                    end_delim = false;");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    start_delim = false;");
            sb.AppendLine("                    end_delim = false;");
            sb.AppendLine("                }");
            sb.AppendLine("            } else if (token_text === '!' && last_type === 'TK_START_EXPR') {");
            sb.AppendLine("                // special case handling: if (!a)");
            sb.AppendLine("                start_delim = false;");
            sb.AppendLine("                end_delim = false;");
            sb.AppendLine("            } else if (last_type === 'TK_OPERATOR') {");
            sb.AppendLine("                start_delim = false;");
            sb.AppendLine("                end_delim = false;");
            sb.AppendLine("            } else if (last_type === 'TK_END_EXPR') {");
            sb.AppendLine("                start_delim = true;");
            sb.AppendLine("                end_delim = true;");
            sb.AppendLine("            } else if (token_text === '.') {");
            sb.AppendLine("                // decimal digits or object.property");
            sb.AppendLine("                start_delim = false;");
            sb.AppendLine("                end_delim = false;");
            sb.AppendLine("");
            sb.AppendLine("            } else if (token_text === ':') {");
            sb.AppendLine("                // zz: xx");
            sb.AppendLine("                // can't differentiate ternary op, so for now it's a ? b: c; without space before colon");
            sb.AppendLine("                if (last_text.match(/^\\d+$/)) {");
            sb.AppendLine("                    // a little help for ternary a ? 1 : 0;");
            sb.AppendLine("                    start_delim = true;");
            sb.AppendLine("                } else {");
            sb.AppendLine("                    start_delim = false;");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            if (start_delim) {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("            print_token();");
            sb.AppendLine("");
            sb.AppendLine("            if (end_delim) {");
            sb.AppendLine("                print_space();");
            sb.AppendLine("            }");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_BLOCK_COMMENT':");
            sb.AppendLine("");
            sb.AppendLine("            print_newline();");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            print_newline();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_COMMENT':");
            sb.AppendLine("");
            sb.AppendLine("            // print_newline();");
            sb.AppendLine("            print_space();");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            print_newline();");
            sb.AppendLine("            break;");
            sb.AppendLine("");
            sb.AppendLine("        case 'TK_UNKNOWN':");
            sb.AppendLine("            print_token();");
            sb.AppendLine("            break;");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        last_type = token_type;");
            sb.AppendLine("        last_text = token_text;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    return output.join('');");
            sb.AppendLine("");
            sb.AppendLine("}");

            return sb.ToString();
        }
        
        #endregion

        //***********************得到数据集并绑定到listViewTables控件************/
        //清空listViewTables
        //设置表头。
        //执行数据库查询操作，得到表中所要显示的数据
        //数据按行绑定到eListView
        /*********************************************************************/
        private void LoadData()
        {
            this.listViewTables.Clear();

            // 针对数据库的字段名称，建立与之适应显示表头 
            listViewTables.Columns.Add("序号", 50, HorizontalAlignment.Left);
            listViewTables.Columns.Add("表名", 180, HorizontalAlignment.Left);
            listViewTables.Columns.Add("表说明", 120, HorizontalAlignment.Left);
            listViewTables.Columns.Add("数据总数", 80, HorizontalAlignment.Left);
            listViewTables.Visible = true;

            // 针对数据库的字段名称，建立与之适应显示表头  避免没选择表时左侧为空
            listViewColumns.Columns.Add("序号", 50, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段名", 120, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段类型", 80, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段说明", 120, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("数据总数", 80, HorizontalAlignment.Left);
            listViewColumns.Visible = true;

            //var sql = " select top 1000" +
            //                   " a.name AS 表名 ," +
            //                   " isnull(g.[value],'-') AS 说明" +
            //                   " from" +
            //                   " sys.tables a left join sys.extended_properties g " +
            //                   "on (a.object_id = g.major_id AND g.minor_id = 0) order by 表名 asc";

            var sql = "SELECT a.name AS 表名, isnull(d.[value],'-') AS 说明, b.rows as 总数"+
                       " FROM sysobjects a WITH(NOLOCK)" +
                       " JOIN sysindexes b WITH(NOLOCK)" +
                       " ON b.id = a.id" +
                       " join sys.tables t" +
                       " on t.object_id=a.id" +
                       " left join sys.extended_properties d" +
                       " on (t.object_id = d.major_id AND d.minor_id = 0)" +
                       " WHERE a.xtype = 'U ' AND b.indid IN (0, 1)" +
                       " ORDER By a.name ASC";

            if (IsMySql)
            {
                sql = "select table_name from information_schema.tables where table_schema='"+ StrDatabase + "'";
            }

            ListViewHelper.DisplayDataSet(listViewTables, GetDataSet(sql), true);
            //int i = 0;
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        i++;
            //        var item = new ListViewItem();
            //        item.SubItems.Clear();
            //        item.SubItems[0].Text = i.ToString();
            //        item.SubItems.Add(row["表名"].ToString());
            //        item.SubItems.Add(row["说明"].ToString());
            //        listViewTables.Items.Add(item);
            //    }


            //}

        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            try
            {
                if (IsMySql)
                {
                    DataTable dt = new DataTable();
                    var read = connection.ExecuteReader("use `" + StrDatabase + "`;" + sql);
                    dt.Load(read);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    Show();
                    return ds;
                }
                else
                {
                    DataSet ds = DbHelperSQL.Query("use [" + StrDatabase + "];" + sql);
                    Show();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                this.Close();
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private void listViewTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTables.SelectedItems.Count > 0)
            {
               
                //MessageBox.Show(listViewTables.SelectedItems[0].SubItems[1].Text+@"-"+listViewTables.SelectedItems[0].SubItems[2].Text);
                StrTableName = listViewTables.SelectedItems[0].SubItems[1].Text;
                
                LoadTableColumns(StrTableName);
                Clipboard.SetDataObject(StrTableName); //则把数据置于剪切板中

                for (int i = 0; i < listViewTables.Items.Count; i++)
                {
                    if (listViewTables.Items[i].Selected == true)
                    {
                        listViewTables.SelectedItems[0].BackColor = Color.DodgerBlue;
                        listViewTables.SelectedItems[0].ForeColor = Color.White;
                    }
                    else
                    {
                        listViewTables.Items[i].BackColor = Color.White;
                        listViewTables.Items[i].ForeColor = Color.Black;
                    }

                }
            }
        }
        /// <summary>
        /// 绑定表字段
        /// </summary>
        private void LoadTableColumns(string tableName)
        {
            this.listViewColumns.Clear();

            // 针对数据库的字段名称，建立与之适应显示表头 
            listViewColumns.Columns.Add("序号", 50, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段名", 120, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段类型", 80, HorizontalAlignment.Left);
            listViewColumns.Columns.Add("字段说明", 150, HorizontalAlignment.Left);
            listViewColumns.Visible = true;
           

            var sql = new StringBuilder();

            sql.AppendLine("select c.name as columnName,t.name as columnType,p.value as columnDescription   from  sysobjects o left join syscolumns c  on o.id=c.id");
            sql.AppendLine(" left join sys.extended_properties p on p.major_id=c.id and p.minor_id=c.colid and p.name='MS_Description' left join systypes t on c.xusertype=t.xusertype");
            sql.AppendLine("where o.type='u' ");
            sql.AppendLine("and o.name='" + tableName + "'");

            if (IsMySql)
            {
                sql = new StringBuilder("select COLUMN_NAME,COLUMN_TYPE,COLUMN_COMMENT  from information_schema.COLUMNS where table_name = '" + tableName +"';");
            }

            ListViewHelper.DisplayDataSet(listViewColumns, GetDataSet(sql.ToString()), true);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(StrDatabase))
                {
                    MessageBox.Show("请先连接数据库！");
                    return;
                }
                //MessageBox.Show(StrTableName);
                richTemplate.Text = "#startLoop model.字段名 = txt字段名.Text;//字段说明 #endLoop";

                listViewTemplate.GridLines = true;//显示各个记录的分隔线 
                listViewTemplate.FullRowSelect = true;//要选择就是一行 
                listViewTemplate.View = View.Details;//定义列表显示的方式 
                listViewTemplate.Scrollable = true;//需要时候显示滚动条 
                listViewTemplate.MultiSelect = false; // 不可以多行选择 
                listViewTemplate.HeaderStyle = ColumnHeaderStyle.Clickable;

                // 针对数据库的字段名称，建立与之适应显示表头
                listViewTemplate.Clear();
                listViewTemplate.Columns.Add("序号", 50, HorizontalAlignment.Left);
                listViewTemplate.Columns.Add("模板", 100, HorizontalAlignment.Left);
                listViewTemplate.Visible = true;
               
                #region 实体

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("using System.Data;");
                sb.AppendLine("using ShoveEIMS3.Site.Service.Base;");
                sb.AppendLine("using Shove.DatabaseFactory;");
                sb.AppendLine("using ShoveEIMS3.Site.Business;");
                sb.AppendLine("");
                sb.AppendLine("namespace ShoveEIMS3.Site.Service");
                sb.AppendLine("{");
                sb.AppendLine("    public class #类名# : ShoveEIMS3.Site.Service.Base.Service, IDisposable");
                sb.AppendLine("    {");
                sb.AppendLine("        #region 属性");
                sb.AppendLine("");
                sb.AppendLine("    	#startLoop");
                sb.AppendLine("        /// <summary>字段说明");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        public long 字段名 { get; set; }");
                sb.AppendLine("    	#endLoop");
                sb.AppendLine("");
                sb.AppendLine("        #endregion");
                sb.AppendLine("");
                sb.AppendLine("        #region 构造函数");
                sb.AppendLine("");
                sb.AppendLine("        public static new string TableName = \"#表名#\";");
                sb.AppendLine("        public static string[] QueryFields = {#startLoop\"字段名\",#endLoop};");
                sb.AppendLine("");
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// 构造函数");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        protected #类名#()");
                sb.AppendLine("            : this(-1) { }");
                sb.AppendLine("");
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// 构造函数");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"id\">运输方式 ID</param>");
                sb.AppendLine("        protected #类名#(long id)");
                sb.AppendLine("        {");
                sb.AppendLine("            base.TableName = TableName;");
                sb.AppendLine("");
                sb.AppendLine("            if (id <= 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                return;");
                sb.AppendLine("            }");
                sb.AppendLine("");
                sb.AppendLine("            if (!OpenFactory(false))");
                sb.AppendLine("            {");
                sb.AppendLine("                return;");
                sb.AppendLine("            }");
                sb.AppendLine("");
                sb.AppendLine("            DataTable dt = null;");
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine("                dt = Factory.Open(TableName, new Factory.FieldCollect(QueryFields), \"Id=\" + id, string.Empty, 0, 0);");
                sb.AppendLine("");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (Exception ex)");
                sb.AppendLine("            {");
                sb.AppendLine("                CloseFactory(true);");
                sb.AppendLine("");
                sb.AppendLine("                SitePublicFunction.RecordError(ex);");
                sb.AppendLine("                return;");
                sb.AppendLine("            }");
                sb.AppendLine("");
                sb.AppendLine("            CloseFactory(false);");
                sb.AppendLine("");
                sb.AppendLine("            if (dt == null || dt.Rows.Count <= 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                if (dt != null) dt.Dispose();");
                sb.AppendLine("");
                sb.AppendLine("                return;");
                sb.AppendLine("            }");
                sb.AppendLine("");
                sb.AppendLine("            Init(dt.Rows[0]);");
                sb.AppendLine("");
                sb.AppendLine("            //释放资源");
                sb.AppendLine("            dt.Dispose();");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// 构造函数,通过外部数据行");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"rowDetail\">表据行</param>");
                sb.AppendLine("        protected #类名# (DataRow rowInfo)");
                sb.AppendLine("        {");
                sb.AppendLine("            base.TableName = TableName;");
                sb.AppendLine("");
                sb.AppendLine("            Init(rowInfo);");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        void Init(DataRow rowInfo)");
                sb.AppendLine("        {");
                sb.AppendLine("            if (rowInfo == null)");
                sb.AppendLine("            {");
                sb.AppendLine("                this.ID = -1;");
                sb.AppendLine("                return;");
                sb.AppendLine("            }");
                sb.AppendLine("");
                sb.AppendLine("			#startLoop");
                sb.AppendLine("	         this.字段名 = rowInfo[\"字段名\"].ToString();");
                sb.AppendLine("	    	#endLoop");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public static #类名# Create()");
                sb.AppendLine("        {");
                sb.AppendLine("            return new #类名#();");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public static #类名# Create(long id)");
                sb.AppendLine("        {");
                sb.AppendLine("            return ServiceFactoryCreator.Create<#类名#>(id, (int)SitePublicEnum.DataCacheTime.门店);");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public static Department Create(DataRow rowInfo)");
                sb.AppendLine("        {");
                sb.AppendLine("            return ServiceFactoryCreator.Create<#类名#>(rowInfo, (int)SitePublicEnum.DataCacheTime.门店);");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public static List<Department> Create(long[] ids)");
                sb.AppendLine("        {");
                sb.AppendLine("            return ServiceFactoryCreator.Create<#类名#, #类名#>(TableName, ids, QueryFields);");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("        #endregion");
                sb.AppendLine("");
                sb.AppendLine("        #region 释放");
                sb.AppendLine("");
                sb.AppendLine("        bool isDispose = false;");
                sb.AppendLine("");
                sb.AppendLine("        ~#类名#()");
                sb.AppendLine("        {");
                sb.AppendLine("            _Dispose();");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public void Dispose()");
                sb.AppendLine("        {");
                sb.AppendLine("            _Dispose();");
                sb.AppendLine("            GC.SuppressFinalize(this);");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        private void _Dispose()");
                sb.AppendLine("        {");
                sb.AppendLine("            if (isDispose) return;");
                sb.AppendLine("");
                sb.AppendLine("            isDispose = true;");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        #endregion");
                sb.AppendLine("");
                sb.AppendLine("        #region 方法");
                sb.AppendLine("");
                sb.AppendLine("        public override bool VerifyAdd(out string erMsg)");
                sb.AppendLine("        {");
                sb.AppendLine("            throw new NotImplementedException();");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine("        public override bool VerifyUpdate(out string erMsg)");
                sb.AppendLine("        {");
                sb.AppendLine("            throw new NotImplementedException();");
                sb.AppendLine("        }");
                sb.AppendLine("        #endregion");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                sb.AppendLine("");
                #endregion

                #region 集合

                StringBuilder sb2 = new StringBuilder();

                sb2.AppendLine("using System;");
                sb2.AppendLine("using System.Collections.Generic;");
                sb2.AppendLine("using System.Text;");
                sb2.AppendLine("using ShoveEIMS3.Site.Service.Base;");
                sb2.AppendLine("using System.Data;");
                sb2.AppendLine("using ShoveEIMS3.Site.Business;");
                sb2.AppendLine("using Shove.DatabaseFactory;");
                sb2.AppendLine("");
                sb2.AppendLine("namespace ShoveEIMS3.Site.Service");
                sb2.AppendLine("{");
                sb2.AppendLine("    public class #类名#Collection : DBFactory, ShoveEIMS3.Site.Service.Base.ICollection<#类名#>");
                sb2.AppendLine("    {");
                sb2.AppendLine("        #region 构造函数");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary>");
                sb2.AppendLine("        /// 构造函数");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        protected #类名#Collection()");
                sb2.AppendLine("        {");
                sb2.AppendLine("");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        public static #类名#Collection Create()");
                sb2.AppendLine("        {");
                sb2.AppendLine("            return new #类名#Collection();");
                sb2.AppendLine("        }");
                sb2.AppendLine("        #endregion");
                sb2.AppendLine("");
                sb2.AppendLine("        public #类名# Add(#类名# t, out string erMsg)");
                sb2.AppendLine("        {");
                sb2.AppendLine("           ");
                sb2.AppendLine("            erMsg = string.Empty;");
                sb2.AppendLine("");
                sb2.AppendLine("            if (!t.VerifyAdd(out erMsg))");
                sb2.AppendLine("            {");
                sb2.AppendLine("                return null;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            Factory.FieldCollect fields = new Factory.FieldCollect(#startLoop \"字段名\",#endLoop);");
                sb2.AppendLine("");
                sb2.AppendLine("            Factory.FieldValueCollect values = new Factory.FieldValueCollect(#startLoop t.字段名,#endLoop");
                sb2.AppendLine("                );");
                sb2.AppendLine("");
                sb2.AppendLine("            long id = -1;");
                sb2.AppendLine("");
                sb2.AppendLine("            if (!OpenFactory(false))");
                sb2.AppendLine("            {");
                sb2.AppendLine("                erMsg = SitePublicFunction.BuildReturnMsg(00001);   //异常错误");
                sb2.AppendLine("");
                sb2.AppendLine("                return null;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            try");
                sb2.AppendLine("            {");
                sb2.AppendLine("                id = Factory.Insert(#表名#, fields, values);");
                sb2.AppendLine("            }");
                sb2.AppendLine("            catch (Exception ex)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                CloseFactory(true);");
                sb2.AppendLine("");
                sb2.AppendLine("                erMsg = string.Empty;");
                sb2.AppendLine("                SitePublicFunction.RecordError(ex);");
                sb2.AppendLine("");
                sb2.AppendLine("                return null;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            t = #类名#.Create(id);");
                sb2.AppendLine("");
                sb2.AppendLine("            if (id < 1)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                CloseFactory(true);");
                sb2.AppendLine("                return null;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            CloseFactory(false);");
                sb2.AppendLine("");
                sb2.AppendLine("            return t;");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        public List<#类名#> ChildrensByObject(string condition, string order, int beginIndex, int pageSize, out string erMsg)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            erMsg = string.Empty;");
                sb2.AppendLine("");
                sb2.AppendLine("            DataTable dt = null;");
                sb2.AppendLine("");
                sb2.AppendLine("            if (!OpenFactory(false))");
                sb2.AppendLine("            {");
                sb2.AppendLine("                erMsg = SitePublicFunction.BuildReturnMsg(00001);   //异常错误");
                sb2.AppendLine("");
                sb2.AppendLine("                return new List<#类名#>();");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            try");
                sb2.AppendLine("            {");
                sb2.AppendLine("                dt = Factory.Open(#类名#.TableName, new Factory.FieldCollect(\"Id\"), condition, string.Empty, beginIndex, pageSize);");
                sb2.AppendLine("            }");
                sb2.AppendLine("            catch (Exception ex)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                CloseFactory(true);");
                sb2.AppendLine("");
                sb2.AppendLine("                erMsg = ex.Message;");
                sb2.AppendLine("                SitePublicFunction.RecordError(ex);");
                sb2.AppendLine("");
                sb2.AppendLine("                return new List<#类名#>();");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            CloseFactory(false);");
                sb2.AppendLine("");
                sb2.AppendLine("            if (dt == null)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                return new List<#类名#>();");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            int index = 0;");
                sb2.AppendLine("            long[] ids = new long[dt.Rows.Count];");
                sb2.AppendLine("");
                sb2.AppendLine("            foreach (DataRow dr in dt.Rows)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                ids[index++] = Shove._Convert.StrToLong(dr[\"Id\"].ToString(), 0);");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            dt.Dispose();");
                sb2.AppendLine("");
                sb2.AppendLine("            return #类名#.Create(ids);");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary> 清除");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        /// <param name=\"erMsg\">返回错误信息</param>");
                sb2.AppendLine("        public bool Clear(out string erMsg)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            erMsg = string.Empty;");
                sb2.AppendLine("");
                sb2.AppendLine("            return Remove(string.Empty, out erMsg);");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary> 删除");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        /// <param name=\"obj\">删除的对象</param>");
                sb2.AppendLine("        /// <param name=\"erMsg\">返回错误信息</param>");
                sb2.AppendLine("        public bool Remove(#类名# obj, out string erMsg)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            erMsg = string.Empty;");
                sb2.AppendLine("");
                sb2.AppendLine("            return Remove(\"Id=\" + obj.ID, out erMsg);");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary> 批量删除");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        /// <param name=\"condition\">条件</param>");
                sb2.AppendLine("        /// <param name=\"erMsg\">返回错误信息</param>");
                sb2.AppendLine("        public bool Remove(string condition, out string erMsg)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            erMsg = string.Empty;");
                sb2.AppendLine("");
                sb2.AppendLine("            List<Favorite> list = ChildrensByObject(condition, string.Empty, 0, 0, out erMsg);");
                sb2.AppendLine("            if (list.Count == 0)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                return true;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            if (!OpenFactory(false))");
                sb2.AppendLine("            {");
                sb2.AppendLine("                erMsg = SitePublicFunction.BuildReturnMsg(00001);   //异常错误");
                sb2.AppendLine("");
                sb2.AppendLine("                return false;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            try");
                sb2.AppendLine("            {");
                sb2.AppendLine("                Factory.Delete(\"#表名#\", condition);");
                sb2.AppendLine("            }");
                sb2.AppendLine("            catch (Exception ex)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                CloseFactory(true);");
                sb2.AppendLine("");
                sb2.AppendLine("                erMsg = ex.Message;");
                sb2.AppendLine("                SitePublicFunction.RecordError(ex);");
                sb2.AppendLine("");
                sb2.AppendLine("                return false;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            CloseFactory(false);");
                sb2.AppendLine("");
                sb2.AppendLine("            return true;");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary>总行数");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        /// <param name=\"condition\">条件</param>");
                sb2.AppendLine("        /// <returns></returns>");
                sb2.AppendLine("        public int Count(string condition)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            DataTable dt = null;");
                sb2.AppendLine("");
                sb2.AppendLine("            if (!OpenFactory(false))");
                sb2.AppendLine("            {");
                sb2.AppendLine("                return 0;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            try");
                sb2.AppendLine("            {");
                sb2.AppendLine("                dt = Factory.Open(\"#表名#\", new Factory.FieldCollect(\"count(1)\"), condition, string.Empty, 0, 0);");
                sb2.AppendLine("            }");
                sb2.AppendLine("            catch (Exception ex)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                CloseFactory(true);");
                sb2.AppendLine("");
                sb2.AppendLine("                SitePublicFunction.RecordError(ex);");
                sb2.AppendLine("                return 0;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            if (dt.Rows == null || dt.Rows.Count <= 0)");
                sb2.AppendLine("            {");
                sb2.AppendLine("                dt.Dispose();");
                sb2.AppendLine("                CloseFactory(false);");
                sb2.AppendLine("                return 0;");
                sb2.AppendLine("            }");
                sb2.AppendLine("");
                sb2.AppendLine("            CloseFactory(false);");
                sb2.AppendLine("");
                sb2.AppendLine("            return Shove._Convert.StrToInt(dt.Rows[0][0].ToString(), 0);");
                sb2.AppendLine("        }");
                sb2.AppendLine("");
                sb2.AppendLine("        /// <summary>总行数(后台专用)");
                sb2.AppendLine("        /// </summary>");
                sb2.AppendLine("        /// <param name=\"condition\">条件</param>");
                sb2.AppendLine("        /// <returns></returns>");
                sb2.AppendLine("        public int BCount(string condition)");
                sb2.AppendLine("        {");
                sb2.AppendLine("            return Count(condition);");
                sb2.AppendLine("        }");
                sb2.AppendLine("    }");
                sb2.AppendLine("}");
                sb2.AppendLine("");

                #endregion

                if (dic.Count==0)
                {
                    dic.Add("Add", "#startLoop model.字段名 = txt字段名.Text;//字段说明 #endLoop");
                    dic.Add("Edit", "#startLoop txt字段名.Text= model.字段名;//字段说明 #endLoop");
                    dic.Add("asp:TextBox", "#startLoop <asp:TextBox id=\"txt字段名\" runat=\"server\" /> #endLoop");
                    dic.Add("HtmlInput", "#startLoop <input type=\"text\" id=\"txt字段名\" name=\"txt字段名\" /> #endLoop");
                    dic.Add("EasyUIColumns", "#startLoop { field:'字段名',title:'字段说明',width:100 }, #endLoop");
                    dic.Add("类", sb.ToString());
                    dic.Add("集合", sb2.ToString());
                }
                int i = 0;
                foreach (var dicItem in dic)
                {
                    i++;
                    var item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = i.ToString();
                    item.SubItems.Add(dicItem.Key);
                    listViewTemplate.Items.Add(item);
                }

            }else if (tabControl1.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(StrDatabase))
                {
                    MessageBox.Show("请先连接数据库！");
                    return;
                }
            }

            labSelectTableName.Text = StrTableName;
        }

        DataTable CurrentDataTale = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (StrTableName=="")
            {
                MessageBox.Show("请选择要操作的表");
                return;
            }

            string className = Regex.Replace(StrTableName, "f8_", string.Empty, RegexOptions.IgnoreCase);
            className= Regex.Replace(className, "t", string.Empty, RegexOptions.IgnoreCase);
            className = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(className);

            StrTableName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(StrTableName);

            var sql = new StringBuilder();

            sql.AppendLine("select c.name as columnName,t.name as columnType,p.value as columnDescription   from  sysobjects o left join syscolumns c  on o.id=c.id");
            sql.AppendLine(" left join sys.extended_properties p on p.major_id=c.id and p.minor_id=c.colid and p.name='MS_Description' left join systypes t on c.xusertype=t.xusertype");
            sql.AppendLine("where o.type='u' ");
            sql.AppendLine("and o.name='" + StrTableName + "'");

            if (IsMySql)
            {
                sql = new StringBuilder("select COLUMN_NAME,COLUMN_TYPE,COLUMN_COMMENT  from information_schema.COLUMNS where table_name = '" + StrTableName + "';");
            }

            //#startLoop[\s\S]*?#endLoop

            var ds = GetDataSet(sql.ToString());
            if (ds!=null&&ds.Tables[0].Rows.Count>0)
            {
                CurrentDataTale = ds.Tables[0];

                var temp = richTemplate.Text;
                temp = Regex.Replace(temp, "#类名#",className, RegexOptions.IgnoreCase);
                temp = Regex.Replace(temp, "#表名#", StrTableName, RegexOptions.IgnoreCase);

                richResult.Text = Regex.Replace(temp, @"#startLoop([\s\S]*?)#endLoop", GetLoopString, RegexOptions.IgnoreCase);
            }
        }

        /// <summary>循环字段
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private string GetLoopString(Match match) {
            
            var result = new StringBuilder();
            foreach (DataRow row in CurrentDataTale.Rows)
            {
                if (IsMySql)
                {
                    result.AppendLine(match.Groups[1].Value.Replace("字段名", row["COLUMN_NAME"].ToString()).Replace("字段说明", row["COLUMN_COMMENT"].ToString()));
                }
                else
                {
                    result.AppendLine(match.Groups[1].Value.Replace("字段名", row["columnName"].ToString()).Replace("字段说明", row["columnDescription"].ToString()));
                }
            }
            return result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(button2.Text); //则把数据置于剪切板中
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(button3.Text); //则把数据置于剪切板中
        }

        private void listViewTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTemplate.SelectedItems.Count > 0)
            {
                //MessageBox.Show(listViewTemplate.SelectedItems[0].SubItems[1].Text);
                richTemplate.Text = dic[listViewTemplate.SelectedItems[0].SubItems[1].Text];

            }
        }

        private void listViewColumns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (listViewColumns.SelectedItems.Count > 0)
                {
                    //将复制的内容放入剪切板中
                    if (listViewColumns.SelectedItems[0].Text != "")
                        Clipboard.SetDataObject(listViewColumns.SelectedItems[0].SubItems[1].Text +" "+ listViewColumns.SelectedItems[0].SubItems[3].Text);
                }
            }
        }

        private void Tables_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK 
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }    
        }

        private void listViewColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewColumns.SelectedItems.Count > 0)
            {
                Clipboard.SetDataObject(textBox1.Text.Trim() + listViewColumns.SelectedItems[0].SubItems[1].Text);
            }
        }
     
        private void listViewTables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listViewTables.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                //if (int.Parse(info.Item.SubItems[3].Text)>0)
                //{
                    StrTableName = info.Item.SubItems[1].Text;
                    //MessageBox.Show(StrTableName);
                    ShowDataForm table = new ShowDataForm();
                    table.Owner = this;
                    table.ShowDialog();
                //}
                //else
                //{
                //    MessageBox.Show("此表木有数据！");
                //}
            } 
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            
               
            if (comboBox1.SelectedItem ==null|| comboBox1.SelectedItem.ToString()=="MySql")
            {
                if (comboBox1.SelectedItem == null)
                {
                    IsMySql = true;
                    _strConn = ";";
                }
                else
                {
                    IsMySql = true;
                    _strConn = "server={0}; user id={1}; password={2}; database=information_schema; port={3}; charset=utf8;pooling=true;Max Pool Size=15;";
                    _strConn = string.Format(_strConn, txtServerUrl.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim(), txt_Port.Text.Trim());
                    _strConn = $"server={ txtServerUrl.Text.Trim()};database=information_schema;uid={txtUser.Text.Trim()};pwd={txtPwd.Text.Trim()};charset='utf8';SslMode=None"; ;
                }

                connection = GetConnection(_strConn);
                BindDatabase();
                btnConnection.Enabled = false;
                btnConfirm.Enabled = true;
                return;

            }

            if (txtUser.Text.Trim() != "" && txtPwd.Text.Trim() != "" && txtServerUrl.Text.Trim() != "")
            {
                try
                {
                    _strConn = "server=" + txtServerUrl.Text.Trim() + ";uid=" + txtUser.Text.Trim() + ";pwd=" + txtPwd.Text.Trim() + ";database=master";

                    DbHelperSQL.connectionString = _strConn;
                    BindDatabase();
                    btnConnection.Enabled = false;
                    btnConfirm.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnConnection.Enabled = true;
                    btnConfirm.Enabled = false;
                }

            }
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns></returns>
        public void BindDatabase()
        {
            DataSet ds;
            if (IsMySql)
            {
                DataTable dt = new DataTable();
                var read = connection.ExecuteReader("show databases;");
                dt.Load(read);
               
                ds = new DataSet();
                ds.Tables.Add(dt);

                comDataBase.DataSource = ds.Tables[0];    //将表绑定到控件
                comDataBase.DisplayMember = "database";     //定义要显示的内容为列名为x的内容
                comDataBase.ValueMember = "database";       //定义要映射的值为y的值

                labDataBase.Visible = true;
                comDataBase.Visible = true;
            }
            else
            {
                ds = DbHelperSQL.Query("select * from [sysdatabases] order by [name]");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    comDataBase.DataSource = ds.Tables[0];    //将表绑定到控件
                    comDataBase.DisplayMember = "name";     //定义要显示的内容为列名为x的内容
                    comDataBase.ValueMember = "dbid";       //定义要映射的值为y的值

                    labDataBase.Visible = true;
                    comDataBase.Visible = true;

                }
            }
           



        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            //tabPage2.Parent = tabPage1.Parent = tabControl1;

            StrDatabase = comDataBase.Text;

             LoadData();
            tabControl1.SelectTab(1);
        }

       
        private void BtnChange_Click_1(object sender, EventArgs e)
        {
            this.txtbBottom.Text = changetext(this.txtbTop.Text);
        }
       
        private string changetext(string strText)
        {
            StringBuilder builder = new StringBuilder();
            string[] strcom = strText.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            char[] cha = { '=', ';' };
            string[] team;
            foreach (string item in strcom)
            {
                if (item.Contains("=") && item.Contains(";"))
                {
                    team = item.Trim().Split(cha);

                    builder.Append(Environment.NewLine);
                    builder.Append(subRep(team[1]) + "=" + team[0] + ";" + team[2]);
                    builder.Append(Environment.NewLine);
                }
                else
                {
                    builder.Append(Environment.NewLine);
                    builder.Append(item);
                    builder.Append(Environment.NewLine);
                }
            }
            return builder.ToString();
        }

        private string subRep(string strt)
        {
            if (strt.IndexOf(".Trim()") == -1)
            {
                return strt;
            }
            else
            {
                return strt.Substring(0, strt.IndexOf(".Trim()"));
            }
        }

        private string Convert0ToCSharp(string strs, string _SourceCode)
        {
            if (string.IsNullOrEmpty(strs))
            {
                strs = "sb";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<%");
            builder.Append(Environment.NewLine);
            builder.Append("StringBuilder " + strs + " = new StringBuilder();");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            string[] strArray = _SourceCode.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < strArray.Length; i++)
            {
                string str = strArray[i].Replace(@"\", @"\\").Replace("\"", "\\\"");
                builder.AppendFormat("" + strs + ".AppendLine(\"{0}\");", str);
                builder.Append(Environment.NewLine);
            }
            builder.Append(Environment.NewLine);
            builder.Append("return " + strs + ".ToString();");
            builder.Append(Environment.NewLine);
            builder.Append("%>");
            return builder.ToString();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.txtbBottom.Text = Convert0ToCSharp(this.TxtbString.Text.Trim(), this.txtbTop.Text);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.txtbTop.Text = "";
            this.txtbBottom.Text = "";
        }

        private void btnaddfile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.FileName.IndexOf(".") > 0)
            {
                this.txtformatxml.Text = FormatXmlFile(this.openFileDialog1.FileName);
            }
            else
            {
                this.txtformatxml.Text = FormatXmlString(this.txtformatxmlsource.Text.Trim());
            }
        }


        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="AString"></param>
        /// <returns></returns>
        public static string FormatXmlString(string AString)
        {
            string str2;
            try
            {
                MemoryStream w = new MemoryStream(0x400);
                XmlTextWriter writer = new XmlTextWriter(w, null);
                XmlDocument document = new XmlDocument();
                writer.Formatting = Formatting.Indented;
                document.LoadXml(AString);
                document.WriteTo(writer);
                writer.Flush();
                writer.Close();
                str2 = Encoding.GetEncoding("utf-8").GetString(w.ToArray());
                w.Close();
            }
            catch (Exception exception1)
            {
                //ProjectData.SetProjectError(exception1);
                //Exception exception = exception1;
                str2 = "";
                //Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                //ProjectData.ClearProjectError();
            }
            return str2;
        }

        /// <summary>
        /// 格式化文件
        /// </summary>
        /// <param name="AFile"></param>
        /// <returns></returns>
        public static string FormatXmlFile(string AFile)
        {
            string str2;
            try
            {
                MemoryStream w = new MemoryStream(0x400);
                XmlTextWriter writer = new XmlTextWriter(w, null);
                XmlDocument document = new XmlDocument();
                writer.Formatting = Formatting.Indented;
                document.Load(AFile);
                document.WriteTo(writer);
                writer.Flush();
                writer.Close();
                str2 = Encoding.GetEncoding("utf-8").GetString(w.ToArray());
                w.Close();
            }
            catch (Exception exception1)
            {
                //ProjectData.SetProjectError(exception1);
                //Exception exception = exception1;
                str2 = "";
                //Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                //ProjectData.ClearProjectError();
            }
            return str2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.txtformatxml.Text = "";
            this.txtformatxmlsource.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectValue = comboBox1.SelectedItem.ToString();
            if (selectValue=="MySql")
            {
                txt_Port.Text = "3306";
                txtUser.Text = "root";
            }
            else
            {
                txt_Port.Text = "";
            }
        }

        private void btnsavefile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //this.Save_Path_Text.Text = this.CheckFileJ.SelectedPath + @"\";
                    File.WriteAllText(this.saveFileDialog1.FileName + "_Format.xml", txtformatxml.Text, Encoding.UTF8);

                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception exception1)
            {
                //ProjectData.SetProjectError(exception1);
                //Exception exception = exception1;
                //Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, null);
                //ProjectData.ClearProjectError();
            }
        }

    }
}
