#pragma checksum "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "218cfc38dc08651a554819f643ab8db6008d98aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auth_ChangePassword), @"mvc.1.0.view", @"/Views/Auth/ChangePassword.cshtml")]
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
#line 1 "C:\Work\CU\CULCS\CULCS\Views\_ViewImports.cshtml"
using CULCS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Work\CU\CULCS\CULCS\Views\_ViewImports.cshtml"
using CULCS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"218cfc38dc08651a554819f643ab8db6008d98aa", @"/Views/Auth/ChangePassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d44d1bdb66c6d2c24c0ce6bb906c702a8316ac0c", @"/Views/_ViewImports.cshtml")]
    public class Views_Auth_ChangePassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CULCS.DTO.ChangePassword2DTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "password", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-parsley-equalto", new global::Microsoft.AspNetCore.Html.HtmlString("#Password"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-parsley"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangePassword", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Auth", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
  
    ViewData["Title"] = "เปลี่ยนรหัสผ่าน";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container-fluid"">
    <div class=""row"">
        <div class=""col-sm-12"">
            <div aria-live=""polite"" aria-atomic=""true"" style=""position: relative;"">
                <div class=""page-title-box"">
                    <div class=""float-right"">
                    </div>
                    <h4 class=""page-title"">เปลี่ยนรหัสผ่าน</h4>
                    <p class=""text-muted"">
                        Change Password
                    </p>
                </div><!--end page-title-box-->
");
#nullable restore
#line 19 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
                 if (ViewBag.ReturnCode != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div style=""position: absolute; top: 10px; right: 0;"">
                        <div class=""toast fade show"" role=""alert"" aria-live=""assertive"" aria-atomic=""true"" data-toggle=""toast"">
                            <div class=""toast-header"">
                                <i");
            BeginWriteAttribute("class", " class=\"", 974, "\"", 1095, 5);
            WriteAttributeValue("", 982, "mdi", 982, 3, true);
            WriteAttributeValue(" ", 985, "mdi-circle-slice-8", 986, 19, true);
            WriteAttributeValue(" ", 1004, "font-18", 1005, 8, true);
            WriteAttributeValue(" ", 1012, "mr-1", 1013, 5, true);
#nullable restore
#line 24 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
WriteAttributeValue(" ", 1017, ViewBag.ReturnCode  == ReturnCode.Success ? "text-success" : "text-danger", 1018, 77, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                                <strong class=\"mr-auto\">");
#nullable restore
#line 25 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
                                                   Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong>
                                <button type=""button"" class=""ml-2 mb-1 close"" data-dismiss=""toast"" aria-label=""Close"">
                                    <span aria-hidden=""true"">&times;</span>
                                </button>
                            </div>
                        </div> <!--end toast-->
                    </div>
");
#nullable restore
#line 32 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div><!--end col-->\r\n    </div>\r\n    <!--end row-->\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "218cfc38dc08651a554819f643ab8db6008d98aa8222", async() => {
                WriteLiteral("\r\n        ");
#nullable restore
#line 38 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
   Write(Html.HiddenFor(m => m.UserID));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""card"">
                    <div class=""card-body mt-3"">
                        <div class=""row"">
                            <div class=""col-lg-6"">
                                <div class=""form-group row"">
                                    <label class=""col-sm-4 col-form-label text-right"">รหัสผ่านใหม่</label>
                                    <div class=""col-sm-8"">
                                        <div class=""input-group"">
                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "218cfc38dc08651a554819f643ab8db6008d98aa9311", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 49 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Password);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                            <span class=""input-group-append"">
                                                <button class=""btn btn-gradient-dark"" type=""button"" onclick=""eyes('Password')""><i id=""eyesPassword"" class=""mdi mdi-eye""></i></button>
                                            </span>
                                        </div>
                                        ");
#nullable restore
#line 54 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
                                   Write(Html.ValidationMessageFor(m => m.Password));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""col-lg-6"">
                                <div class=""form-group row"">
                                    <label class=""col-sm-12 text-secondary"">รหัสผ่านต้องไม่น้อยกว่า 8 ตัวและไม่เกิน 12 ตัว<br />ประกอบด้วยตัวเลขอย่างน้อย 1 ตัว และตัวอักษรอย่างน้อย 1 ตัว</label>
                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-lg-6"">
                                <div class=""form-group row"">
                                    <label class=""col-sm-4 col-form-label text-right"">ยืนยันรหัสผ่าน</label>
                                    <div class=""col-sm-8"">
                                        <div class=""input-group"">
                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "218cfc38dc08651a554819f643ab8db6008d98aa13071", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 70 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ConfirmPassword);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                            <span class=""input-group-append"">
                                                <button class=""btn btn-gradient-dark"" type=""button"" onclick=""eyes('ConfirmPassword')""><i id=""eyesConfirmPassword"" class=""mdi mdi-eye""></i></button>
                                            </span>
                                        </div>
                                        ");
#nullable restore
#line 75 "C:\Work\CU\CULCS\CULCS\Views\Auth\ChangePassword.cshtml"
                                   Write(Html.ValidationMessageFor(m => m.ConfirmPassword));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-lg-6"">
                                <div class=""form-group row"">
                                    <label class=""col-sm-4 col-form-label text-right""></label>
                                    <div class=""col-sm-8"">
                                        <button class=""btn btn-primary"" type=""submit"">บันทึก</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> <!-- end card-body -->
                </div> <!-- end card -->
            </div> <!-- end col -->
            <!-- end col -->
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n    <!-- end col -->\r\n</div> <!-- end row -->\r\n<!-- end page content -->\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        function eyes(id) {
            if ($('#' + id).get(0).type == 'password') {
                $('#' + id).get(0).type = 'text';
                $('#eyes' + id).removeClass();
                $('#eyes' + id).addClass(""mdi mdi-eye-off"");
            }
            else {
                $('#' + id).get(0).type = 'password';
                $('#eyes' + id).removeClass();
                $('#eyes' + id).addClass(""mdi mdi-eye"");
            }
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CULCS.DTO.ChangePassword2DTO> Html { get; private set; }
    }
}
#pragma warning restore 1591