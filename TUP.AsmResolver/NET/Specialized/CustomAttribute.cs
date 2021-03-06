﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TUP.AsmResolver.NET.Specialized
{
    public class CustomAttribute : MetaDataMember
    {
        MetaDataMember parent;
        MethodReference constructor;
        CustomAttributeSignature signature;

        public MetaDataMember Parent
        {
            get
            {
                if (parent == null)
                {
                    MetaDataMember member;
                    tablereader.HasCustomAttribute.TryGetMember(Convert.ToInt32(metadatarow.parts[0]), out member);
                    parent = member;
                }
                return parent;
            }
        }
        public MethodReference Constructor
        {
            get
            {
                if (constructor == null)
                {
                    MetaDataMember member;
                    tablereader.CustomAttributeType.TryGetMember(Convert.ToInt32(metadatarow.parts[1]), out member);
                    constructor = (MethodReference)member;
                }
                return constructor;
            }
        }
        public uint Value
        {
            get { return Convert.ToUInt32(metadatarow.parts[2]); }
        }

        public CustomAttributeSignature Signature
        {
            get
            {
                if (signature == null)
                {
                    signature = netheader.BlobHeap.ReadCustomAttributeSignature(this, Value);
                }
                return signature;
            }
        }
    
        public override string ToString()
        {
            return Constructor.FullName;
        }
        public override void ClearCache()
        {
            parent = null;
            constructor = null;
            signature = null;
        }
    }
}
