// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Internal.TypeSystem
{
    /// <summary>
    /// When implemented by a <see cref="TypeDesc"/>, instructs a name mangler to use the same mangled name
    /// as another entity while prepending a specific prefix to that mangled name.
    /// </summary>
    public interface IPrefixMangledType
    {
        /// <summary>
        /// Type whose mangled name to use.
        /// </summary>
        TypeDesc BaseType { get; }

        /// <summary>
        /// Prefix to apply when mangling.
        /// </summary>
        string Prefix { get; }
    }
}
