// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void MultiplyAddSingle()
        {
            var test = new SimpleTernaryOpTest__MultiplyAddSingle();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Sse.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Sse.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Sse.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleTernaryOpTest__MultiplyAddSingle
    {
        private static readonly int LargestVectorSize = 16;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector128<Single>>() / sizeof(Single);
        private static readonly int Op2ElementCount = Unsafe.SizeOf<Vector128<Single>>() / sizeof(Single);
        private static readonly int Op3ElementCount = Unsafe.SizeOf<Vector128<Single>>() / sizeof(Single);
        private static readonly int RetElementCount = Unsafe.SizeOf<Vector128<Single>>() / sizeof(Single);

        private static Single[] _data1 = new Single[Op1ElementCount];
        private static Single[] _data2 = new Single[Op2ElementCount];
        private static Single[] _data3 = new Single[Op3ElementCount];

        private static Vector128<Single> _clsVar1;
        private static Vector128<Single> _clsVar2;
        private static Vector128<Single> _clsVar3;

        private Vector128<Single> _fld1;
        private Vector128<Single> _fld2;
        private Vector128<Single> _fld3;

        private SimpleTernaryOpTest__DataTable<Single, Single, Single, Single> _dataTable;

        static SimpleTernaryOpTest__MultiplyAddSingle()
        {
            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _clsVar1), ref Unsafe.As<Single, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _clsVar2), ref Unsafe.As<Single, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _clsVar3), ref Unsafe.As<Single, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());
        }

        public SimpleTernaryOpTest__MultiplyAddSingle()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _fld1), ref Unsafe.As<Single, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _fld2), ref Unsafe.As<Single, byte>(ref _data2[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = (float)(random.NextDouble()); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Single>, byte>(ref _fld3), ref Unsafe.As<Single, byte>(ref _data3[0]), (uint)Unsafe.SizeOf<Vector128<Single>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = (float)(random.NextDouble()); }
            for (var i = 0; i < Op2ElementCount; i++) { _data2[i] = (float)(random.NextDouble()); }
            for (var i = 0; i < Op3ElementCount; i++) { _data3[i] = (float)(random.NextDouble()); }
            _dataTable = new SimpleTernaryOpTest__DataTable<Single, Single, Single, Single>(_data1, _data2, _data3, new Single[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => Fma.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Fma.MultiplyAdd(
                Unsafe.Read<Vector128<Single>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector128<Single>>(_dataTable.inArray2Ptr),
                Unsafe.Read<Vector128<Single>>(_dataTable.inArray3Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            var result = Fma.MultiplyAdd(
                Sse.LoadVector128((Single*)(_dataTable.inArray1Ptr)),
                Sse.LoadVector128((Single*)(_dataTable.inArray2Ptr)),
                Sse.LoadVector128((Single*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            var result = Fma.MultiplyAdd(
                Sse.LoadAlignedVector128((Single*)(_dataTable.inArray1Ptr)),
                Sse.LoadAlignedVector128((Single*)(_dataTable.inArray2Ptr)),
                Sse.LoadAlignedVector128((Single*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAdd), new Type[] { typeof(Vector128<Single>), typeof(Vector128<Single>), typeof(Vector128<Single>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<Single>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector128<Single>>(_dataTable.inArray2Ptr),
                                        Unsafe.Read<Vector128<Single>>(_dataTable.inArray3Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Single>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAdd), new Type[] { typeof(Vector128<Single>), typeof(Vector128<Single>), typeof(Vector128<Single>) })
                                     .Invoke(null, new object[] {
                                        Sse.LoadVector128((Single*)(_dataTable.inArray1Ptr)),
                                        Sse.LoadVector128((Single*)(_dataTable.inArray2Ptr)),
                                        Sse.LoadVector128((Single*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Single>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            var result = typeof(Fma).GetMethod(nameof(Fma.MultiplyAdd), new Type[] { typeof(Vector128<Single>), typeof(Vector128<Single>), typeof(Vector128<Single>) })
                                     .Invoke(null, new object[] {
                                        Sse.LoadAlignedVector128((Single*)(_dataTable.inArray1Ptr)),
                                        Sse.LoadAlignedVector128((Single*)(_dataTable.inArray2Ptr)),
                                        Sse.LoadAlignedVector128((Single*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector128<Single>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            var result = Fma.MultiplyAdd(
                _clsVar1,
                _clsVar2,
                _clsVar3
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _clsVar3, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var firstOp = Unsafe.Read<Vector128<Single>>(_dataTable.inArray1Ptr);
            var secondOp = Unsafe.Read<Vector128<Single>>(_dataTable.inArray2Ptr);
            var thirdOp = Unsafe.Read<Vector128<Single>>(_dataTable.inArray3Ptr);
            var result = Fma.MultiplyAdd(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            var firstOp = Sse.LoadVector128((Single*)(_dataTable.inArray1Ptr));
            var secondOp = Sse.LoadVector128((Single*)(_dataTable.inArray2Ptr));
            var thirdOp = Sse.LoadVector128((Single*)(_dataTable.inArray3Ptr));
            var result = Fma.MultiplyAdd(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            var firstOp = Sse.LoadAlignedVector128((Single*)(_dataTable.inArray1Ptr));
            var secondOp = Sse.LoadAlignedVector128((Single*)(_dataTable.inArray2Ptr));
            var thirdOp = Sse.LoadAlignedVector128((Single*)(_dataTable.inArray3Ptr));
            var result = Fma.MultiplyAdd(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleTernaryOpTest__MultiplyAddSingle();
            var result = Fma.MultiplyAdd(test._fld1, test._fld2, test._fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunFldScenario()
        {
            var result = Fma.MultiplyAdd(_fld1, _fld2, _fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _fld3, _dataTable.outArrayPtr);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector128<Single> firstOp, Vector128<Single> secondOp, Vector128<Single> thirdOp, void* result, [CallerMemberName] string method = "")
        {
            Single[] inArray1 = new Single[Op1ElementCount];
            Single[] inArray2 = new Single[Op2ElementCount];
            Single[] inArray3 = new Single[Op3ElementCount];
            Single[] outArray = new Single[RetElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref inArray1[0]), firstOp);
            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref inArray2[0]), secondOp);
            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref inArray3[0]), thirdOp);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Single, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Single>>());

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(void* firstOp, void* secondOp, void* thirdOp, void* result, [CallerMemberName] string method = "")
        {
            Single[] inArray1 = new Single[Op1ElementCount];
            Single[] inArray2 = new Single[Op2ElementCount];
            Single[] inArray3 = new Single[Op3ElementCount];
            Single[] outArray = new Single[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Single, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(firstOp), (uint)Unsafe.SizeOf<Vector128<Single>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Single, byte>(ref inArray2[0]), ref Unsafe.AsRef<byte>(secondOp), (uint)Unsafe.SizeOf<Vector128<Single>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Single, byte>(ref inArray3[0]), ref Unsafe.AsRef<byte>(thirdOp), (uint)Unsafe.SizeOf<Vector128<Single>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Single, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Single>>());

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(Single[] firstOp, Single[] secondOp, Single[] thirdOp, Single[] result, [CallerMemberName] string method = "")
        {
            if (BitConverter.SingleToInt32Bits(MathF.Round((firstOp[0] * secondOp[0]) + thirdOp[0], 3)) != BitConverter.SingleToInt32Bits(MathF.Round(result[0], 3)))
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if (BitConverter.SingleToInt32Bits(MathF.Round((firstOp[i] * secondOp[i]) + thirdOp[i], 3)) != BitConverter.SingleToInt32Bits(MathF.Round(result[i], 3)))
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Fma)}.{nameof(Fma.MultiplyAdd)}<Single>(Vector128<Single>, Vector128<Single>, Vector128<Single>): {method} failed:");
                Console.WriteLine($"   firstOp: ({string.Join(", ", firstOp)})");
                Console.WriteLine($"  secondOp: ({string.Join(", ", secondOp)})");
                Console.WriteLine($"   thirdOp: ({string.Join(", ", thirdOp)})");
                Console.WriteLine($"    result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}
