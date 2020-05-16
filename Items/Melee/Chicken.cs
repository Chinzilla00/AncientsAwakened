using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class Chicken : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rubber Chicken");
        }

		public override void SetDefaults()
		{
			item.damage = 30;
			item.melee = true;
			item.width = 54;
			item.height = 60;
			item.useTime = 25;
            item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 1000;
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.rare = ItemRarityID.Purple;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/Chicken"), player.Center);
        }
    }
}
