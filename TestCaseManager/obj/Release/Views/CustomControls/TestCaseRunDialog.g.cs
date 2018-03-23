﻿#pragma checksum "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6F762C601C07B42A5CF878D3ACE4F8197EF92DE8"
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
    /// TestCaseRunDialog
    /// </summary>
    public partial class TestCaseRunDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TestCaseManager.Views.CustomControls.TestCaseRunDialog DragWindow;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ProjectGrid1;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CurrentTestCaseLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusComboBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TestCasePanelBorder;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TestCaseEditView;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderTestCaseName;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TestCaseTitle;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderTestCasePriority;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PriorityComboBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderTestCaseSeverity;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SeverityComboBox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderTestCaseAutomated;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IsAutomatedCheckBox;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TestCaseEditViewBorder;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox TestStepList;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RegisterIssueButton;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveRunButton;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/customcontrols/testcaserundialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
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
            this.DragWindow = ((TestCaseManager.Views.CustomControls.TestCaseRunDialog)(target));
            return;
            case 2:
            this.ProjectGrid1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 18 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PreviousTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CurrentTestCaseLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            
            #line 20 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StatusComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.TestCasePanelBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.TestCaseEditView = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.BorderTestCaseName = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.TestCaseTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.BorderTestCasePriority = ((System.Windows.Controls.Border)(target));
            return;
            case 12:
            this.PriorityComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.BorderTestCaseSeverity = ((System.Windows.Controls.Border)(target));
            return;
            case 14:
            this.SeverityComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 15:
            this.BorderTestCaseAutomated = ((System.Windows.Controls.Border)(target));
            return;
            case 16:
            this.IsAutomatedCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 17:
            this.TestCaseEditViewBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 18:
            this.TestStepList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 19:
            this.RegisterIssueButton = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
            this.RegisterIssueButton.Click += new System.Windows.RoutedEventHandler(this.RegisterIssue_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            this.saveRunButton = ((System.Windows.Controls.Button)(target));
            
            #line 120 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
            this.saveRunButton.Click += new System.Windows.RoutedEventHandler(this.SaveRunStatus_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\..\Views\CustomControls\TestCaseRunDialog.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.Cancel);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

