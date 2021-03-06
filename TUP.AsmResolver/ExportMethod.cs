﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUP.AsmResolver.ASM;
namespace TUP.AsmResolver
{
    /// <summary>
    /// Represents a procedure or method that is inside a portable executable file and can be used by other PE files.
    /// </summary>
    public struct ExportMethod : IMethod 
    {
        internal ExportMethod(string lib, string name, uint rva, uint ordinal)
        {
            this.lib = lib;
            this.name = name;
            this.rva = rva;
            this.ordinal = ordinal;
        }
        private string lib;
        private string name;
        
        private uint rva;
        private uint ordinal;

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name
        {
            get { return name; }
        }        
        /// <summary>
        /// Gets the name of the declaring library of the method.
        /// </summary>
        public string LibraryName
        {
            get { return System.IO.Path.GetFileName(lib); }
        }
        /// <summary>
        /// Gets the full name of the method, including the declaring library and method name.
        /// </summary>
        public string FullName
        {
            get { return LibraryName + "." + Name; }
        }
        /// <summary>
        /// Gets the Relative Virtual Address of the method.
        /// </summary>
        public uint RVA
        {
            get { return rva; }
        }
        /// <summary>
        /// Gets the ordinal of the method.
        /// </summary>
        public uint Ordinal
        {
            get { return ordinal; }
        }

        
        /// <summary>
        /// Returns the string representation of the exportable method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FullName;
        }


        
    }
}
