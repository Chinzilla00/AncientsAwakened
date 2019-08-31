using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Dev
{
    //Ferret's dev weapon (axe form)
    public class CordesDuFuret_Axe : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cordes Du Furet");
			Tooltip.SetDefault("Right click in inventory to change between firing notes and smashing heads\n'YA ne delayu DRUGOY NULEVOY POVTOR.'");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 1;
			item.knockBack = 7;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.axe = 50;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
			byte pre = item.prefix;
            item.TurnToAir();
			int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("CordesDuFuret_Notes"), 1, false, pre, false, false);
            Main.item[itemID].newAndShiny = false;
            if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
			}
        }

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }
		
		public override Vector2? HoldoutOrigin()
		{
			return new Vector2(16, 52);
		}	
	}
}
