using System;
using OtterGui.Classes;

namespace GlamSiphon.Events;

// this file is straight up taken from Glamourer.

/// <summary>
/// Triggered when Penumbra is reloaded.
/// </summary>
public sealed class PenumbraReloaded : EventWrapper<Action, PenumbraReloaded.Priority>
{
    public enum Priority
    {
        /// <seealso cref="Interop.ChangeCustomizeService.Restore"/>
        ChangeCustomizeService = 0,
    }

    public PenumbraReloaded()
        : base(nameof(PenumbraReloaded))
    { }

    public void Invoke()
        => Invoke(this);
}
