#pragma checksum "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b2ec1b2b0ebdcc371aacfbeab6fcf1acebc2ba6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Product_Delete), @"mvc.1.0.view", @"/Areas/Admin/Views/Product/Delete.cshtml")]
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
#nullable restore
#line 1 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\_ViewImports.cshtml"
using e_commerce;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\_ViewImports.cshtml"
using e_commerce.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\_ViewImports.cshtml"
using e_commerce.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b2ec1b2b0ebdcc371aacfbeab6fcf1acebc2ba6", @"/Areas/Admin/Views/Product/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19f9eac0d01fbed12d2cf7c57d7094a3b7a7a1df", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Product_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onerror", new global::Microsoft.AspNetCore.Html.HtmlString("this.onerror=null;this.src=\'/img/deleted.png\'"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Slider", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("forms-sample"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: flex; justify-content: center;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12 grid-margin stretch-card"" style=""margin-bottom:0"">
        <div class=""card"">
            <div class=""card-body"">
                <div class=""table-responsive"">
                    <table class=""table table-striped"">
                        <thead>
                            <tr>
                                <th>

                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Price
                                </th>
                                <th>
                                    Category
                                </th>
                                <th>
                                    SubCategory
                                </th>
                                <th>
                                    Brend
                                </th>
 ");
            WriteLiteral(@"                               <th>
                                    Specs
                                </th>
                                <th>
                                    IsDeleted
                                </th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6b2ec1b2b0ebdcc371aacfbeab6fcf1acebc2ba68370", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1622, "~/img/", 1622, 6, true);
#nullable restore
#line 45 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
AddHtmlAttributeValue("", 1628, Model.Images.ToList()[0].ImageName, 1628, 35, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 48 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 51 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 54 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 57 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.CategoryChild.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 60 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Brend.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 63 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Specs.ProducerForView);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 63 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                                              Write(Model.Specs.ProducerValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                                    ");
#nullable restore
#line 64 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Specs.ProductionYearForView);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 64 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                                                    Write(Model.Specs.ProductionYearValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                                    ");
#nullable restore
#line 65 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Specs.TypeForView);

#line default
#line hidden
#nullable disable
            WriteLiteral(" :  ");
#nullable restore
#line 65 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                                           Write(Model.Specs.TypeValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                                    ");
#nullable restore
#line 66 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                               Write(Model.Specs.OSForView);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 66 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                                        Write(Model.Specs.OSValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n");
#nullable restore
#line 69 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                     if (Model.IsDeleted == true)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <i class=\"mdi mdi-check\" style=\"font-size: 30px; color: #71C016;\"></i>\r\n");
#nullable restore
#line 72 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <i class=\"mdi mdi-close\" style=\"font-size: 30px; color: #FF4747;\"></i>\r\n");
#nullable restore
#line 76 "C:\Users\IDEAPAD\Desktop\Full_Stack_Project\e-commerce\Areas\Admin\Views\Product\Delete.cshtml"

                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-lg-12 grid-margin stretch-card"">
        <div class=""card"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b2ec1b2b0ebdcc371aacfbeab6fcf1acebc2ba615972", async() => {
                WriteLiteral("\r\n                <button type=\"submit\" class=\"btn btn-danger mr-2\">\r\n                    <i class=\"mdi mdi-delete\"></i>\r\n                </button>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b2ec1b2b0ebdcc371aacfbeab6fcf1acebc2ba616406", async() => {
                    WriteLiteral("\r\n                    <i class=\"mdi mdi-close\"></i>\r\n                ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Product> Html { get; private set; }
    }
}
#pragma warning restore 1591