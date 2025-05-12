using System;
using Content.Shared.Coordinates;
using Content.Shared.Damage;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Humanoid;
using Content.Shared.Stealth.Components;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;

namespace Content.Shared.SS220.Misc;

public sealed class GavelStandSystem : EntitySystem
{
    [Dependency] private readonly TagSystem _tag = default!;
    [Dependency] private readonly EntityLookupSystem _entityLookup = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly SharedHandsSystem _hands = default!;

    private static readonly ProtoId<TagPrototype> JudgeGavelTag = "JudgeGavel";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GavelStandComponent, DamageModifyEvent>(OnGavelDamaged);
    }

    private void OnGavelDamaged(Entity<GavelStandComponent> gavel_stand, ref DamageModifyEvent args)
    {
        if (!args.Origin.HasValue)
            return;

        var owner = (EntityUid)args.Origin;
        var item = _hands.GetActiveItem(owner);

        if (!item.HasValue)
            return;

        if (_tag.HasTag((EntityUid)item, JudgeGavelTag))
        {
            ExclamateAround(gavel_stand, owner, (GavelStandComponent)gavel_stand);
        }
    }

    private void ExclamateTarget(EntityUid target, GavelStandComponent component)
    {
        SpawnAttachedTo(component.Effect, target.ToCoordinates());
    }

    private void ExclamateAround(EntityUid gavel_stand, EntityUid owner, GavelStandComponent component)
    {
        StealthComponent? stealth = null;

        foreach (var iterator in
            _entityLookup.GetEntitiesInRange<HumanoidAppearanceComponent>(_transform.GetMapCoordinates(gavel_stand), component.Distance))
        {
            //Avoid pinging invisible entities
            if (TryComp(iterator, out stealth) && stealth.Enabled)
                continue;

            //We don't want to ping user of whistle
            if (iterator.Owner == owner)
                continue;

            ExclamateTarget(iterator, component);
        }
    }
}
