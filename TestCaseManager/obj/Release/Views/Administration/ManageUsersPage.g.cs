﻿#pragma checksum "..\..\..\..\Views\Administration\ManageUsersPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D9A64D92383DB800EED21767AB15EC5BC617AC3EB290715E74D408FFAB8101F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TestCaseManager.BindingConverters;
using TestCaseManager.Views.CustomControls.MicrosoftAutoComplete;


namespace TestCaseManager.Views.Administration {
    
    
    /// <summary>
    /// ManageUsersPage
    /// </summary>
    public partial class ManageUsersPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelConvertionProvider;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TestCaseManager.Views.CustomControls.MicrosoftAutoComplete.AutoCompleteTextBox AutoCompleteBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Username;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IsAdminCheckBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MessageLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/administration/manageuserspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
            ((TestCaseManager.Views.Administration.ManageUsersPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StackPanelConvertionProvider = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.AutoCompleteBox = ((TestCaseManager.Views.CustomControls.MicrosoftAutoComplete.AutoCompleteTextBox)(target));
            return;
            case 4:
            
            #line 18 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectUser_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 7:
            this.IsAdminCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            
            #line 40 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditUser_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.MessageLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            
            #line 52 "..\..\..\..\Views\Administration\ManageUsersPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteUser_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

