using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Ohno
{
    public class ono : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ono");
			Tooltip.SetDefault("ono");
		}

		public override void SetDefaults() 
		{
			item.width = 16;
			item.height = 16;
			item.accessory = true;
			item.value = 100;
			item.rare = -1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			AAPlayer p = player.GetModPlayer<AAPlayer>();
			p.ono = true;
			if (hideVisual) 
			{
				p.onoHideVanity = true;
			}
		}
	}

	public class onoHead : EquipTexture
	{
		public override bool DrawHead() 
		{
			return false;
		}
	}

	public class onoBody : EquipTexture
	{
		public override bool DrawBody() 
		{
			return false;
		}
	}

	public class onoLegs : EquipTexture
	{
		public override bool DrawLegs() 
		{
			return false;
		}
	}
}