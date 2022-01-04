//
// Copyright (c) 2010-2021 Antmicro
//
// This file is licensed under the MIT License.
// Full license text is available in 'licenses/MIT.txt'.
//
namespace Antmicro.Renode.Peripherals.CPU
{
    // If modified, make sure tlib's 'export.c:tlib_update_execution_mode' remains correct.
    public enum ExecutionMode
    {
        Continuous,
        SingleStepNonBlocking,
        SingleStepBlocking,
    }
}

