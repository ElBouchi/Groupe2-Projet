#pragma checksum "..\..\..\..\View\addWork.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D61387684E39D709EDCF2309EA6B9D97D876E71A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Projet;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Projet.View {
    
    
    /// <summary>
    /// addWork
    /// </summary>
    public partial class addWork : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sourcePATH;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox destPATH;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox backupType;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uploadSource;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uploadDestination;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label destLabel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nameLabel;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label sourceLabel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\View\addWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label backupTypeLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Projet_vzbnilnp_wpftmp;component/view/addwork.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\addWork.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\View\addWork.xaml"
            ((Projet.View.addWork)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Add = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\View\addWork.xaml"
            this.Add.Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.sourcePATH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.destPATH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.backupType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.uploadSource = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\View\addWork.xaml"
            this.uploadSource.Click += new System.Windows.RoutedEventHandler(this.uploadSource_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.uploadDestination = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\View\addWork.xaml"
            this.uploadDestination.Click += new System.Windows.RoutedEventHandler(this.uploadDestination_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.destLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.nameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.sourceLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.backupTypeLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

