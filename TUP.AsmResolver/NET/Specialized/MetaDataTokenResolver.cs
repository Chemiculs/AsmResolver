﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TUP.AsmResolver.NET.Specialized
{

    /// <summary>
    /// A class that is able to look up members or strings by its metadata token.
    /// </summary>
    public class MetaDataTokenResolver
    {
        NETHeader netheader;

        public MetaDataTokenResolver(NETHeader netheader)
        {
            this.netheader = netheader;
        }

        public object ResolveToken(int metadataToken)
        {
            byte rowIndex = (byte)(metadataToken >> 0x18);
            if (rowIndex == 0x70)
                return ResolveString(metadataToken);
            else
                return ResolveMember(metadataToken);
        }

        /// <summary>
        /// Resolves a member by its metadata token.
        /// </summary>
        /// <param name="metadataToken">The token of the member to look up.</param>
        /// <returns></returns>
        public MetaDataMember ResolveMember(int metadataToken)
        {
            if (metadataToken == 0)
                throw new ArgumentException("Cannot resolve a member from a zero metadata token", "metadataToken");

            MetaDataTableType tabletype = (MetaDataTableType)(metadataToken >> 0x18);

            if (!netheader.tableheap.HasTable(tabletype))
                throw new ArgumentException("Table is not present in tables heap.");

            int subtraction = ((int)tabletype) * 0x1000000;
            int rowindex = metadataToken - subtraction;
            return netheader.TablesHeap.GetTable( tabletype).members[rowindex - 1];
        }
        /// <summary>
        /// Resolves a string value by its metadata token.
        /// </summary>
        /// <param name="metadataToken">The token of the string value to look up.</param>
        /// <returns></returns>
        public string ResolveString(int metadataToken)
        {
            int actualindex = metadataToken - 0x70000000;
            return netheader.UserStringsHeap.GetStringByOffset((uint)actualindex);
            
        }


    }
}
