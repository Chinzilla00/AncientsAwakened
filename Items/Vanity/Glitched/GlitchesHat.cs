using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Glitched
{
    [AutoloadEquip(EquipType.Head)]
	public class GlitchesHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gl17cH's Helmet");
			Tooltip.SetDefault("Great for impersonating AA devs!");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.rare = 10;
			item.vanity = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("GlitchesBreastplate") && legs.type == mod.ItemType("GlitchesGreaves");
		}
	}
}