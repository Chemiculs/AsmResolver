﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TUP.AsmResolver.NET.Specialized
{
    public class Constant : MetaDataMember
    {
        object value;
        MetaDataMember parent;

        public ElementType ConstantType
        {
            get { return (Specialized.ElementType)Convert.ToByte(metadatarow.parts[0]); }
        }
        public MetaDataMember Parent
        {
            get 
            {
                if (parent != null || tablereader.HasConstant.TryGetMember(Convert.ToInt32(metadatarow.parts[2]), out parent))
                    return parent;
                
                return null;
            }
        }
        public uint Signature
        {
            get { return Convert.ToUInt32(metadatarow.parts[3]); }
        }
        public object Value
        {
            get
            {
                if (value == null)
                    value = netheader.BlobHeap.ReadConstantValue(ConstantType, Signature);
                return value;

            }
        }
        public override void ClearCache()
        {
            value = null;
            parent = null;
        }

    }
}
