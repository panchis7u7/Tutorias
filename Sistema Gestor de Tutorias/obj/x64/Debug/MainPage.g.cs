﻿#pragma checksum "E:\Proyectos\C#\Tutorias C#\Sistema Gestor de Tutorias\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "05B8A4F3D3E1A6688A10B3A77E6CC3E56A93087D5CDBA2DC567F8426D77ACBE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistema_Gestor_de_Tutorias
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    this.mainPageRef = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // MainPage.xaml line 19
                {
                    this.navMenu = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.navMenu).BackRequested += this.navMenu_BackRequested;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.navMenu).Loaded += this.navMenu_Loaded;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.navMenu).SelectionChanged += this.navMenu_SelectionChanged;
                }
                break;
            case 3: // MainPage.xaml line 35
                {
                    this.ASB = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 4: // MainPage.xaml line 39
                {
                    this.btn_home = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 5: // MainPage.xaml line 44
                {
                    this.btn_Consultas = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 6: // MainPage.xaml line 49
                {
                    this.btn_formatos = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 7: // MainPage.xaml line 55
                {
                    this.hola = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 8: // MainPage.xaml line 56
                {
                    this.main_frame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

