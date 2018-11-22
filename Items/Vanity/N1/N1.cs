using Terraria.ModLoader;

namespace AAMod.Items.Vanity.N1
{
    [AutoloadEquip(EquipType.Body)]
    public class N1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Number One Jumpsuit");
            Tooltip.SetDefault(
@"You are number 1
'In memory of Stefan Karl Stefansson'");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
            item.rare = 11;
            item.expert = true;
            item.vanity = true;
        }

        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            robes = true;
            // The equipSlot is added in AAMod.cs --> Load hook
            equipSlot = mod.GetEquipSlot("N1_Legs", EquipType.Legs);
        }

    }
}