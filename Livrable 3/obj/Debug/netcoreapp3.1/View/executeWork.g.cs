#pragma checksum "..\..\..\..\View\executeWork.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "96B1FDE239E94A2479939FAAC71DB340B951FEC2"
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
    /// executeWork
    /// </summary>
    public partial class executeWork : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Works;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uniqueExec;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Play;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Pause;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Stop;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\View\executeWork.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Delete;
        
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
            System.Uri resourceLocater = new System.Uri("/Projet;component/view/executework.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\executeWork.xaml"
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
            
            #line 8 "..\..\..\..\View\executeWork.xaml"
            ((Projet.View.executeWork)(target)).Loaded += new System.Windows.RoutedEventHandler(this.executeWork_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Works = ((System.Windows.Controls.DataGrid)(target));
            
            #line 10 "..\..\..\..\View\executeWork.xaml"
            this.Works.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.Works_LoadingRow);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\..\View\executeWork.xaml"
            this.Works.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.Works_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.uniqueExec = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\View\executeWork.xaml"
            this.uniqueExec.Click += new System.Windows.RoutedEventHandler(this.uniqueExec_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Play = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\View\executeWork.xaml"
            this.Play.Click += new System.Windows.RoutedEventHandler(this.Play_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Pause = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\View\executeWork.xaml"
            this.Pause.Click += new System.Windows.RoutedEventHandler(this.Pause_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Stop = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\View\executeWork.xaml"
            this.Stop.Click += new System.Windows.RoutedEventHandler(this.Stop_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Delete = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\View\executeWork.xaml"
            this.Delete.Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

