using Content.Client.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Client.GameObjects;

namespace Content.Client.Weapons.Ranged.Systems;

public sealed partial class GunSystem
{
    private void InitializeSpentAmmo()
    {
        SubscribeLocalEvent<SpentAmmoVisualsComponent, AppearanceChangeEvent>(OnSpentAmmoAppearance);
    }

    private void OnSpentAmmoAppearance(EntityUid uid, SpentAmmoVisualsComponent component, ref AppearanceChangeEvent args)
    {
        var sprite = args.Sprite;
        if (sprite == null) return;

        if (!args.AppearanceData.TryGetValue(AmmoVisuals.Spent, out var varSpent))
        {
            return;
        }

        var spent = (bool) varSpent;
        string? state = null;

        if (spent && component.State != null)
            state = component.Suffix ? $"{component.State}-spent" : "spent";
        else
            state = component.State;
        
        if (state != null)
            sprite.LayerSetState(AmmoVisualLayers.Base, state);
        if (!component.Tip && sprite.LayerExists(AmmoVisualLayers.Tip))
            sprite.RemoveLayer(AmmoVisualLayers.Tip);
        
        if (sprite.LayerExists(AmmoVisualLayers.Spent))
            sprite.LayerSetVisible(AmmoVisualLayers.Spent, spent);
    }
}
