﻿#pragma checksum "E:\Proyectos\C#\Tutorias C#\Sistema Gestor de Tutorias\Pagina_Grupos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4A33F33F5DFCA10014019075D81CEC686A3D6808815D000F8A36B0BDB17DA180"
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
    partial class Pagina_Grupos : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBox_Text(global::Windows.UI.Xaml.Controls.TextBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class Pagina_Grupos_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPagina_Grupos_Bindings
        {
            private global::Sistema_Gestor_de_Tutorias.Pagina_Grupos dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.GridView obj3;
            private global::Windows.UI.Xaml.Controls.TextBox obj7;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj3ItemsSourceDisabled = false;
            private static bool isobj7TextDisabled = false;

            private Pagina_Grupos_obj1_BindingsTracking bindingsTracking;

            public Pagina_Grupos_obj1_Bindings()
            {
                this.bindingsTracking = new Pagina_Grupos_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 80 && columnNumber == 19)
                {
                    isobj3ItemsSourceDisabled = true;
                }
                else if (lineNumber == 64 && columnNumber == 161)
                {
                    isobj7TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // Pagina_Grupos.xaml line 75
                        this.obj3 = (global::Windows.UI.Xaml.Controls.GridView)target;
                        break;
                    case 7: // Pagina_Grupos.xaml line 64
                        this.obj7 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_7(this.obj7);
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IPagina_Grupos_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Sistema_Gestor_de_Tutorias.Pagina_Grupos)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Sistema_Gestor_de_Tutorias.Pagina_Grupos obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_GruposItems(obj.GruposItems, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_InfoGruposTutores(obj.InfoGruposTutores, phase);
                    }
                }
            }
            private void Update_GruposItems(global::System.Collections.ObjectModel.ObservableCollection<global::Sistema_Gestor_de_Tutorias.Modelos.GruposItem> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Grupos.xaml line 75
                    if (!isobj3ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj3, obj, null);
                    }
                }
            }
            private void Update_InfoGruposTutores(global::Sistema_Gestor_de_Tutorias.Database_Assets.InfoGruposTutor obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_InfoGruposTutores_grupo(obj.grupo, phase);
                    }
                }
            }
            private void Update_InfoGruposTutores_grupo(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Pagina_Grupos.xaml line 64
                    if (!isobj7TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj7, obj, null);
                    }
                }
            }
            private void UpdateTwoWay_7_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.InfoGruposTutores != null)
                        {
                            this.dataRoot.InfoGruposTutores.grupo = this.obj7.Text;
                        }
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class Pagina_Grupos_obj1_BindingsTracking
            {
                private global::System.WeakReference<Pagina_Grupos_obj1_Bindings> weakRefToBindingObj; 

                public Pagina_Grupos_obj1_BindingsTracking(Pagina_Grupos_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<Pagina_Grupos_obj1_Bindings>(obj);
                }

                public Pagina_Grupos_obj1_Bindings TryGetBindingObject()
                {
                    Pagina_Grupos_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                }

                public void RegisterTwoWayListener_7(global::Windows.UI.Xaml.Controls.TextBox sourceObject)
                {
                    sourceObject.LostFocus += (sender, e) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_7_Text();
                        }
                    };
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Pagina_Grupos.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Pagina_Grupos.xaml line 53
                {
                    this.AgregarGruposPopup = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                    ((global::Windows.UI.Xaml.Controls.Primitives.Popup)this.AgregarGruposPopup).Loaded += this.AgregarGruposPopup_Loaded;
                    ((global::Windows.UI.Xaml.Controls.Primitives.Popup)this.AgregarGruposPopup).LayoutUpdated += this.AgregarGruposPopup_LayoutUpdated;
                }
                break;
            case 3: // Pagina_Grupos.xaml line 75
                {
                    this.GruposGrid = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.GruposGrid).ItemClick += this.GruposGrid_ItemClick;
                }
                break;
            case 6: // Pagina_Grupos.xaml line 58
                {
                    this.relativeChild = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 7: // Pagina_Grupos.xaml line 64
                {
                    this.txtbx_Grupo = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // Pagina_Grupos.xaml line 65
                {
                    this.chkbx_Profesor = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.chkbx_Profesor).Unchecked += this.chkbx_Profesor_Unchecked;
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.chkbx_Profesor).Checked += this.chkbx_Profesor_Checked;
                }
                break;
            case 9: // Pagina_Grupos.xaml line 66
                {
                    this.cmbbx_Profesores = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 10: // Pagina_Grupos.xaml line 67
                {
                    this.cmbbx_Psicologo = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 11: // Pagina_Grupos.xaml line 68
                {
                    this.cmbbx_carrera = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 12: // Pagina_Grupos.xaml line 69
                {
                    this.txtbx_Semestre = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // Pagina_Grupos.xaml line 70
                {
                    this.btn_Agregar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_Agregar).Tapped += this.btn_Agregar_Tapped;
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
            switch(connectionId)
            {
            case 1: // Pagina_Grupos.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Pagina_Grupos_obj1_Bindings bindings = new Pagina_Grupos_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

