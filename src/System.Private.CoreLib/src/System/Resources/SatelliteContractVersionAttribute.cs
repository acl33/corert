﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*============================================================
**
** 
** 
**
**
** Purpose: Specifies which version of a satellite assembly 
**          the ResourceManager should ask for.
**
**
===========================================================*/

using Internal.Runtime.CompilerServices;
using System;
using System.Diagnostics.Contracts;

namespace System.Resources
{
    [RelocatedType("System.Resources.ResourceManager")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public sealed class SatelliteContractVersionAttribute : Attribute
    {
        private String _version;

        public SatelliteContractVersionAttribute(String version)
        {
            if (version == null)
                throw new ArgumentNullException("version");
            Contract.EndContractBlock();
            _version = version;
        }

        public String Version
        {
            get { return _version; }
        }
    }
}