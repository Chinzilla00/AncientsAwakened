using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Mech
{
    [AutoloadEquip(EquipType.Body)]
	public class MechBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mechanical Breastplate");
			Tooltip.SetDefault("5% increased critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.rare = 5;
			item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 5;
		}
	}
}

		/*public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			Texture2D texture = mod.GetTexture("Glowmasks/MechBody_Body_Glowmask");
			{
				color = drawPlayer.GetImmuneAlphaPure(Color.White, shadow);
			}
		}*/