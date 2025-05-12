// © SS220, An EULA/CLA with a hosting restriction, full text: https://raw.githubusercontent.com/SerbiaStrong-220/space-station-14/master/CLA.txt

using System;
using Robust.Shared.GameStates;

namespace Content.Shared.SS220.Misc;

/// <summary>
/// Component that apply exclamation to people around when recieve hit from weapon with tag JudgeGavel
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class GavelStandComponent : Component
{
    [DataField("effect")]
    public string Effect = "Exclamation";

    [DataField]
    public float Distance = 5;
}
