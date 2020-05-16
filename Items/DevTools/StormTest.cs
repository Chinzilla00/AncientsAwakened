using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Items.DevTools.Cinematic;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class StormTest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Feather Test");
            BaseUtility.AddTooltips(item, new string[] { "Feathers" });					
		}			
		
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 0;
            item.rare = ItemRarityID.Purple;
            item.autoReuse = false;
            item.useTurn = true;
            item.expert = true; item.expertOnly = true;
            item.shootSpeed = 9f;
            item.shoot = 1;
            item.noUseGraphic = true;
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int a = 0; a < 8; a++)
            {
                Dust.NewDust(player.Center, player.width, player.height, ModContent.DustType<Feather2>(), Main.rand.Next(-2, 2), 1, 0);
            }
            return false;
		}
    }
}