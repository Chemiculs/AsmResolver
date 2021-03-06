﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TUP.AsmResolver.NET.Specialized
{
    public class MethodReference : MemberReference, IGenericParametersProvider
    {
        internal MethodSignature signature = null;
        GenericParameter[] genericparams = null;
        TypeReference declaringType = null;
        string name = null;

        public override TypeReference DeclaringType
        {
            get
            {
                if (declaringType == null)
                    declaringType = (TypeReference)tablereader.MemberRefParent.GetMember(Convert.ToInt32(metadatarow.parts[0]));
                return declaringType;
            }
        }
        public override string Name
        {
            get
            {
                if (name == null)
                    name = netheader.StringsHeap.GetStringByOffset(Convert.ToUInt32(metadatarow.parts[1]));
                return name;
            }
        }
        public virtual MethodSignature Signature
        {
            get
            {
                if (signature != null)
                    return signature;
                signature = (MethodSignature)netheader.blobheap.ReadMemberRefSignature(Convert.ToUInt32(metadatarow.parts[2]),this);
                return signature;
                //return Convert.ToUInt32(metadatarow.parts[2]); 
            }
        }
        public virtual bool IsDefinition
        {
            get { return false; }
        }
        public virtual bool IsGenericMethod
        {
            get { return false; }
        }
        public virtual GenericParameter[] GenericParameters
        {
            get
            {
                return null;
            }
        }

        public override string FullName
        {
            get
            {
                try
                {

                    if (DeclaringType is TypeReference)
                        return Signature.ReturnType.ToString() + " " + ((TypeReference)DeclaringType).FullName + "::" + Name + Signature.GetParameterString();

                    return Name;
                }
                catch { return Name; }
            }
        }

        public override string ToString()
        {
            return FullName;
        }

        public override void ClearCache()
        {
            signature = null;
            genericparams = null;
            declaringType = null;
            name = null;
        }
    }
}
