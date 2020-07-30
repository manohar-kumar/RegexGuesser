#pragma checksum "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\GameScreen.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c85750c14528a9250b4a225e0408f6c56cbe18e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GameScreen), @"mvc.1.0.view", @"/Views/Home/GameScreen.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/GameScreen.cshtml", typeof(AspNetCore.Views_Home_GameScreen))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\_ViewImports.cshtml"
using RegexService;

#line default
#line hidden
#line 2 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\_ViewImports.cshtml"
using RegexService.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c85750c14528a9250b4a225e0408f6c56cbe18e3", @"/Views/Home/GameScreen.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b8b160c40e011b8c04d1b7c034665b9694badcb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GameScreen : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/submit.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\GameScreen.cshtml"
  
    ViewData["Title"] = "Game Screen";

#line default
#line hidden
            BeginContext(47, 3181, true);
            WriteLiteral(@"

<div id=""question-portal"">
    <p>Input your regex as a string below</p>
    <input type=""text"" id=""RegexString"" />
    <br />
    <p>Input strings matching the regex</p>
    <ul id=""matchStrings"">
        <li>
            <input type=""text"" id=""match1"" />
        </li>
        <li>
            <input type=""text"" id=""match2"" />
        </li>
    </ul>
    <br />
    <p>Input strings that do not match your regex</p>
    <ul id=""nomatchStrings"">
        <li>
            <input type=""text"" id=""nomatch1"" />
        </li>
        <li>
            <input type=""text"" id=""nomatch2"" />
        </li>
    </ul>
    <p>
        <button type=""button"" id=""SubmitRegex"">
            Challenge
        </button>
    </p>
</div>

<div id=""question-display"">
    <p>Below are the strings that match with the asked regex</p>
    <ul id=""matcherStrings""></ul>
    <p>Below are the strings that do not match with the asked regex</p>
    <ul id=""nomatcherStrings""></ul>

    <p>
        Enter your");
            WriteLiteral(@" guess
    </p>
    <input type=""text"" id=""RegexAnswer"" required />

    <p>
        <button type=""button"" id=""SubmitAnswer"">
            Guess
        </button>
    </p>
    <br />
        <button type=""button"" id=""Hint"">
            Need Help, Ask For More Strings
        </button>
</div>
<div id=""Result"" style=""font-size:larger;color:burlywood;background-color:dimgray;font-weight:bold;position:relative;margin-left:30px;margin-top:10px;display:none"">
    <p id=""Winner"" style=""position:relative; margin-left:30px;"">You Guessed it Right.</p>
    <p id=""Points"" style=""position:relative; margin-left:30px;""></p>
</div>
<div id=""btnholder"">
    <button id=""Reset"">RESET</button>
</div>
<style>
    *,
    :before,
    :after {
        box-sizing: border-box;
    }
    ul {
        list-style-type: none
    }
    input {
        background: none;
        color: #807e7e;
        font-size: 18px;
        padding: 10px 10px 10px 5px;
        display: block;
        width: 320px;
    ");
            WriteLiteral(@"    border: none;
        border-radius: 0;
        border-bottom: 1px solid #c6c6c6;
    }
    #btnholder{
        position:relative;
    }
    #Reset{
        position:absolute;
        right:20px;
        font-size:larger;
        border:thick;
        background-color:dimgray;
        color:antiquewhite;
    }
    #question-portal p, #question-display p {
        border: 2px solid #0066FF;
        background-color: #6eb5a9;
        padding: 5px;
        margin-top: 5px;
        margin-bottom: 5px;
        margin-left: 20px;
        margin-right: 20px;
        font-family: Verdana;
        font-size:16px;
    }
    #RegexString,#RegexAnswer
    {
        position: relative;
        margin-left:50px;
        margin-top:20px;
    }

    #Hint {
        position: relative;
        margin-left: 100px;
        background-color: #bbbd4d;
        font-size: 18px;
    }
    #SubmitRegex{
        font-style:italic;
        font-weight:bold;
    }
    #matcherStrings, #noma");
            WriteLiteral("tcherStrings{\r\n        font-family:Arial, Helvetica, sans-serif;\r\n        font-size:large;\r\n    }\r\n</style>\r\n");
            EndContext();
            BeginContext(3228, 60, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b57f3b973d3346ec9edad0d220c3fdeb", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3288, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(3290, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21c62a16e3f342f9b94e60878e5c239e", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3328, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591