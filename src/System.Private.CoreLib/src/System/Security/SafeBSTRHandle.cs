// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Security
{
    internal sealed class SafeBSTRHandle : SafeBuffer
    {
        internal SafeBSTRHandle() : base(true) { }

        internal static SafeBSTRHandle Allocate(uint lenInChars)
        {
            ulong lenInBytes = lenInChars * sizeof(char);
            SafeBSTRHandle bstr = Interop.OleAut32.SysAllocStringLen(IntPtr.Zero, lenInChars);
            if (bstr.IsInvalid) // SysAllocStringLen returns a NULL ptr when there's insufficient memory
            {
                throw new OutOfMemoryException();
            }
            bstr.Initialize(lenInBytes);
            return bstr;
        }

        override protected bool ReleaseHandle()
        {
            Interop.NtDll.ZeroMemory(handle, (UIntPtr)(Interop.OleAut32.SysStringLen(handle) * sizeof(char)));
            Interop.OleAut32.SysFreeString(handle);
            return true;
        }

        internal unsafe void ClearBuffer()
        {
            byte* bufferPtr = null;
            try
            {
                AcquirePointer(ref bufferPtr);
                Interop.NtDll.ZeroMemory((IntPtr)bufferPtr, (UIntPtr)(Interop.OleAut32.SysStringLen((IntPtr)bufferPtr) * sizeof(char)));
            }
            finally
            {
                if (bufferPtr != null)
                {
                    ReleasePointer();
                }
            }
        }

        internal unsafe uint Length => Interop.OleAut32.SysStringLen(this);

        internal unsafe static void Copy(SafeBSTRHandle source, SafeBSTRHandle target, uint bytesToCopy)
        {
            if (bytesToCopy == 0)
            {
                return;
            }

            byte* sourcePtr = null, targetPtr = null;
            try
            {
                source.AcquirePointer(ref sourcePtr);
                target.AcquirePointer(ref targetPtr);

                Debug.Assert(source.ByteLength >= bytesToCopy, "Source buffer is too small.");
                Buffer.MemoryCopy(sourcePtr, targetPtr, target.ByteLength, bytesToCopy);
            }
            finally
            {
                if (targetPtr != null)
                {
                    target.ReleasePointer();
                }
                if (sourcePtr != null)
                {
                    source.ReleasePointer();
                }
            }
        }
    }
}
