﻿#pragma checksum "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EA9B7A9B377C12A618D3D07140612B9B7BE8BFB0"
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
using TestCaseManager.Core.Proxy;


namespace TestCaseManager.Views.CustomControls {
    
    
    /// <summary>
    /// GitHubRepoSelectorDialog
    /// </summary>
    public partial class GitHubRepoSelectorDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TestCaseManager.Views.CustomControls.GitHubRepoSelectorDialog DragWindow;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel GitHubSuccessfulyCreatedPanel;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CreatedLabel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CopyToClipboard;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel IssueRegistrationPanel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GitHubUsername;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox GitHubPassword;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InvalidCredentials;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GitHubRepositoryList;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateIssue;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/customcontrols/githubreposelectordialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.DragWindow = ((TestCaseManager.Views.CustomControls.GitHubRepoSelectorDialog)(target));
            return;
            case 2:
            this.GitHubSuccessfulyCreatedPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.CreatedLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.CopyToClipboard = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
            this.CopyToClipboard.Click += new System.Windows.RoutedEventHandler(this.CopyToClipboard_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.IssueRegistrationPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 6:
            this.GitHubUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.GitHubPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 8:
            
            #line 35 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadRepositories_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.InvalidCredentials = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.GitHubRepositoryList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.CreateIssue = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
            this.CreateIssue.Click += new System.Windows.RoutedEventHandler(this.CreateIssue_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\Views\CustomControls\GitHubRepoSelectorDialog.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.Cancel);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

