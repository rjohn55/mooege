﻿/*
 * Copyright (C) 2011 mooege project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System.Collections.Generic;
using System.Linq;
using bnet.protocol.presence;

namespace Mooege.Core.MooNet.Helpers
{
    public class FieldKeyHelper
    {
        public enum Program : uint
        {
            BNet = 16974,
            D3 = 17459
        }

        public enum OriginatingClass : uint
        {
            Account = 1,
            GameAccount = 2,
            Hero = 3,
            Channel = 4,
            Unknown = 5
        }

        public static FieldKey Create(Program program, OriginatingClass originatingClass, uint field, ulong index)
        {
            return
                FieldKey.CreateBuilder().SetProgram((uint) program).SetGroup((uint) originatingClass).SetField(
                    field).SetIndex(index).Build();
        }


        private HashSet<FieldKey> _changedFields = new HashSet<FieldKey>();
        private Dictionary<FieldKey, FieldOperation> _FieldValues = new Dictionary<FieldKey, FieldOperation>();

        public void SetFieldValue(FieldKey key, FieldOperation operation)
        {
            if (!_changedFields.Contains(key))
                _changedFields.Add(key);

            _FieldValues[key] = operation;
        }

        public void SetFieldValue(Program program, OriginatingClass originatingClass, uint field, ulong index, FieldOperation operation)
        {
            var key = Create(program, originatingClass, field, index);
            this.SetFieldValue(key, operation);
        }


        public List<FieldOperation> GetChangedFieldList()
        {
            return new List<FieldOperation>(_FieldValues.Values);
        }

        public void ClearChanged()
        {
            this._changedFields.Clear();
            this._FieldValues.Clear();
        }

    } 
}