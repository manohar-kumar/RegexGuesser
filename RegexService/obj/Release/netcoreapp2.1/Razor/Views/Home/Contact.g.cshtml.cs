#pragma checksum "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b1c2a6789aec12c128b0915e6ab87a7df314af86"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Contact.cshtml", typeof(AspNetCore.Views_Home_Contact))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1c2a6789aec12c128b0915e6ab87a7df314af86", @"/Views/Home/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b8b160c40e011b8c04d1b7c034665b9694badcb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1, true);
            WriteLiteral("﻿");
            EndContext();
#line 1 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\Contact.cshtml"
   
    ViewData["Title"] = "GamePlay";

#line default
#line hidden
            BeginContext(45, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(50, 17, false);
#line 4 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\Contact.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(67, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(79, 19, false);
#line 5 "C:\Users\manokuma\source\repos\RegexService\RegexService\Views\Home\Contact.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(98, 835, true);
            WriteLiteral(@"</h3>

<p>
    <ol>
        <li>At anytime click on Home to start queueing again</li>
        <li>Queue for a random game by pressing the button on home screen and selecting a unique name</li>
        <li>
            If you are a asker,
            <ul>
                <li>Think of a regex string</li>
                <li>Give some strings matching and not matching to the regex</li>
            </ul>
        </li>
        <li>
            If you are a responder,
            <ul>
                <li>Then wait for the asker to give the problem</li>
                <li>Guess the correct regex or ask for more strings from the asker</li>
            </ul>
        </li>
    </ol>
</p>

<style>
    ul,ol {
        font-display:block;
        font-size:medium;
        font-family:Arial;
    }
</style>
");
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
