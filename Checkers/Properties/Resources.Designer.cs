﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Checkers.Properties {
    using System;
    
    
    /// <summary>
    ///   Třída prostředků se silnými typy pro vyhledávání lokalizovaných řetězců atp.
    /// </summary>
    // Tato třída byla automaticky generována třídou StronglyTypedResourceBuilder
    // pomocí nástroje podobného aplikaci ResGen nebo Visual Studio.
    // Chcete-li přidat nebo odebrat člena, upravte souboru .ResX a pak znovu spusťte aplikaci ResGen
    // s parametrem /str nebo znovu sestavte projekt aplikace Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Vrací instanci ResourceManager uloženou v mezipaměti použitou touto třídou.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Checkers.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Potlačí vlastnost CurrentUICulture aktuálního vlákna pro všechna
        ///   vyhledání prostředků pomocí třídy prostředků se silnými typy.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap kamen_blue {
            get {
                object obj = ResourceManager.GetObject("kamen_blue", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap kamen_blue_queen {
            get {
                object obj = ResourceManager.GetObject("kamen_blue_queen", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap kamen_red {
            get {
                object obj = ResourceManager.GetObject("kamen_red", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap kamen_red_queen {
            get {
                object obj = ResourceManager.GetObject("kamen_red_queen", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.IO.UnmanagedMemoryStream podobný System.IO.MemoryStream.
        /// </summary>
        public static System.IO.UnmanagedMemoryStream Sliding {
            get {
                return ResourceManager.GetStream("Sliding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Vyhledává lokalizovaný prostředek typu System.IO.UnmanagedMemoryStream podobný System.IO.MemoryStream.
        /// </summary>
        public static System.IO.UnmanagedMemoryStream Swooshing {
            get {
                return ResourceManager.GetStream("Swooshing", resourceCulture);
            }
        }
    }
}
