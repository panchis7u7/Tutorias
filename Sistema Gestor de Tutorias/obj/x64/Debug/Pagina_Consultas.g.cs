﻿#pragma checksum "E:\Proyectos\C#\Tutorias C#\Sistema Gestor de Tutorias\Pagina_Consultas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "81DBAAD0E0CAE11234C515C45CB518F391CEF8A2B6140757BCB691D9697C27C5"
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
    partial class Pagina_Consultas : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
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
        private class Pagina_Consultas_obj13_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPagina_Consultas_Bindings
        {
            private global::Sistema_Gestor_de_Tutorias.InfoAlumnos dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj13;
            private global::Windows.UI.Xaml.Controls.TextBlock obj14;
            private global::Windows.UI.Xaml.Controls.TextBlock obj15;
            private global::Windows.UI.Xaml.Controls.TextBlock obj16;
            private global::Windows.UI.Xaml.Controls.TextBlock obj17;
            private global::Windows.UI.Xaml.Controls.TextBlock obj18;
            private global::Windows.UI.Xaml.Controls.TextBlock obj19;
            private global::Windows.UI.Xaml.Controls.TextBlock obj20;
            private global::Windows.UI.Xaml.Controls.TextBlock obj21;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj14TextDisabled = false;
            private static bool isobj15TextDisabled = false;
            private static bool isobj16TextDisabled = false;
            private static bool isobj17TextDisabled = false;
            private static bool isobj18TextDisabled = false;
            private static bool isobj19TextDisabled = false;
            private static bool isobj20TextDisabled = false;
            private static bool isobj21TextDisabled = false;

            public Pagina_Consultas_obj13_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 60 && columnNumber == 50)
                {
                    isobj14TextDisabled = true;
                }
                else if (lineNumber == 61 && columnNumber == 57)
                {
                    isobj15TextDisabled = true;
                }
                else if (lineNumber == 62 && columnNumber == 54)
                {
                    isobj16TextDisabled = true;
                }
                else if (lineNumber == 63 && columnNumber == 40)
                {
                    isobj17TextDisabled = true;
                }
                else if (lineNumber == 64 && columnNumber == 40)
                {
                    isobj18TextDisabled = true;
                }
                else if (lineNumber == 65 && columnNumber == 40)
                {
                    isobj19TextDisabled = true;
                }
                else if (lineNumber == 66 && columnNumber == 40)
                {
                    isobj20TextDisabled = true;
                }
                else if (lineNumber == 67 && columnNumber == 40)
                {
                    isobj21TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 13: // Pagina_Consultas.xaml line 59
                        this.obj13 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.StackPanel)target);
                        break;
                    case 14: // Pagina_Consultas.xaml line 60
                        this.obj14 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 15: // Pagina_Consultas.xaml line 61
                        this.obj15 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 16: // Pagina_Consultas.xaml line 62
                        this.obj16 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 17: // Pagina_Consultas.xaml line 63
                        this.obj17 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 18: // Pagina_Consultas.xaml line 64
                        this.obj18 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 19: // Pagina_Consultas.xaml line 65
                        this.obj19 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 20: // Pagina_Consultas.xaml line 66
                        this.obj20 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 21: // Pagina_Consultas.xaml line 67
                        this.obj21 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj13.Target as global::Windows.UI.Xaml.Controls.StackPanel).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::Sistema_Gestor_de_Tutorias.InfoAlumnos) item, 1 << phase);
            }

            public void Recycle()
            {
            }

            // IPagina_Consultas_Bindings

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
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Sistema_Gestor_de_Tutorias.InfoAlumnos)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Sistema_Gestor_de_Tutorias.InfoAlumnos obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_id_alumno(obj.id_alumno, phase);
                        this.Update_matricula(obj.matricula, phase);
                        this.Update_nombre(obj.nombre, phase);
                        this.Update_apellidos(obj.apellidos, phase);
                        this.Update_semestre(obj.semestre, phase);
                        this.Update_carrera(obj.carrera, phase);
                        this.Update_cod_postal(obj.cod_postal, phase);
                        this.Update_provincia(obj.provincia, phase);
                    }
                }
            }
            private void Update_id_alumno(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 60
                    if (!isobj14TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj14, obj.ToString(), null);
                    }
                }
            }
            private void Update_matricula(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 61
                    if (!isobj15TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj15, obj.ToString(), null);
                    }
                }
            }
            private void Update_nombre(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 62
                    if (!isobj16TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj16, obj, null);
                    }
                }
            }
            private void Update_apellidos(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 63
                    if (!isobj17TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj17, obj, null);
                    }
                }
            }
            private void Update_semestre(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 64
                    if (!isobj18TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj18, obj.ToString(), null);
                    }
                }
            }
            private void Update_carrera(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 65
                    if (!isobj19TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj19, obj, null);
                    }
                }
            }
            private void Update_cod_postal(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 66
                    if (!isobj20TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj20, obj.ToString(), null);
                    }
                }
            }
            private void Update_provincia(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Pagina_Consultas.xaml line 67
                    if (!isobj21TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj21, obj, null);
                    }
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
            case 1: // Pagina_Consultas.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Pagina_Consultas.xaml line 34
                {
                    this.InventoryList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // Pagina_Consultas.xaml line 46
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element4 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element4).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element4).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element4).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 5: // Pagina_Consultas.xaml line 47
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element5 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element5).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element5).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element5).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 6: // Pagina_Consultas.xaml line 48
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element6 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element6).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element6).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element6).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 7: // Pagina_Consultas.xaml line 49
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element7 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element7).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element7).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element7).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 8: // Pagina_Consultas.xaml line 50
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element8 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element8).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element8).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element8).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 9: // Pagina_Consultas.xaml line 51
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element9 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element9).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element9).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element9).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 10: // Pagina_Consultas.xaml line 52
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element10 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element10).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element10).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element10).PointerExited += this.TextBlock_PointerExited;
                }
                break;
            case 11: // Pagina_Consultas.xaml line 53
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element11 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element11).PointerReleased += this.TextBlock_PointerReleased;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element11).PointerEntered += this.TextBlock_PointerEntered;
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element11).PointerExited += this.TextBlock_PointerExited;
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
            case 13: // Pagina_Consultas.xaml line 59
                {                    
                    global::Windows.UI.Xaml.Controls.StackPanel element13 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    Pagina_Consultas_obj13_Bindings bindings = new Pagina_Consultas_obj13_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element13.DataContext);
                    element13.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element13, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element13, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

