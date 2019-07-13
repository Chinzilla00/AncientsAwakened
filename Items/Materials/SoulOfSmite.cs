using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class SoulOfSmite : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Smite");
			Tooltip.SetDefault("The essence of Inferno creatures");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		// TODO -- Velocity Y smaller, post NewItem?
		public override void SetDefaults()
		{
			Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofNight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = refItem.rare;
        }

        public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
		}
	}
}