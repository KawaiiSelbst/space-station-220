using System;
using Robust.Shared.GameStates;

namespace Content.Shared.SS220.Misc;

[RegisterComponent, NetworkedComponent]
public sealed partial class GavelStandComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("effect")]
    public string Effect = "Exclamation";

    [DataField]
    public float Distance = 0;
}
