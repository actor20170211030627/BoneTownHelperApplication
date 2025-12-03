using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("骨头镇修改器(Bowntown TRainer)-actor2015")]     // 程序集标题（显示在程序属性的“文件说明”）
[assembly: AssemblyDescription("添加资源, 自由飞天, 开灯, 晚上改白天...")]  // 程序集描述（显示在“描述”）
[assembly: AssemblyConfiguration("")]                                    // 配置（如 Debug/Release，通常留空）
[assembly: AssemblyCompany("命运")]                                       // 公司名称（显示在“公司”）
[assembly: AssemblyProduct("骨头镇修改器(Bowntown TRainer)-actor2015")]   // 产品名称（显示在“产品名称”，通常与标题一致）
[assembly: AssemblyCopyright("Copyright ©  2024")]                       // 版权信息（显示在“版权”）
[assembly: AssemblyTrademark("https://tieba.baidu.com/f?kw=bonetown")]   // 商标信息（显示在“商标”，可选）, 可填官网?
[assembly: AssemblyCulture("")]                                          // 文化/语言（如 zh-CN，中性文化留空）

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
// COM 互操作相关（WPF 通常无需修改）
[assembly: ComVisible(false)]                                   // 是否对 COM 可见（WPF 一般设为 false）

//In order to begin building localizable applications, set
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


// WPF 特定：是否允许程序集内的 XAML 资源被其他程序集访问
[assembly: ThemeInfo(
    //                                 主题特定资源字典的位置（None 表示无）
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    //                                          通用资源字典的位置（当前程序集）
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page,
    // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.1.0")]              // 程序集版本（用于程序集引用和强命名）
[assembly: AssemblyFileVersion("1.2.1.0")]          // 文件版本（显示在程序属性的“文件版本”）
[assembly: AssemblyInformationalVersion("1.2.1.0(20251118)")]   // 信息版本（显示在“产品版本”，可包含额外信息）