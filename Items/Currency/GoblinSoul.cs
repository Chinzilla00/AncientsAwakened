using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Currency
{
    public class GoblinSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Soul");
			Tooltip.SetDefault("The soul of a goblin");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
        
        public override void SetDefaults()
		{
			item.width = 16;
            item.height = 16;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 3;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.ForestGreen.ToVector3() * 0.55f * Main.essScale);
		}
	}
}