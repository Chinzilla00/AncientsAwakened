using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;


namespace AAMod.Items.Boss.Rajah
{
    public class RajahSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Sash of Vengeance");
            Tooltip.SetDefault(@"Every 10% of health lost gives you 8% extra attack power to your highest damage type boost
Increased Jump Height and Speed
Grants Autojump
Immunity to fall damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true;
        }

        public override void UpdateEquip(Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            if (modPlayer.MeleeHighest(player))
            {
                player.meleeDamage += DamageBoost(player); 
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.rangedDamage += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.magicDamage += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.minionDamage += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.thrownDamage += DamageBoost(player);
            }
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax * .1f)
            {
                return .72f;
            }
            if (player.statLife <= player.statLifeMax * .2f)
            {
                return .64f;
            }
            if (player.statLife <= player.statLifeMax * .3f)
            {
                return .56f;
            }
            if (player.statLife <= player.statLifeMax * .4f)
            {
                return .48f;
            }
            if (player.statLife <= player.statLifeMax * .5f)
            {
                return .4f;
            }
            if (player.statLife <= player.statLifeMax * .6f)
            {
                return .32f;
            }
            if (player.statLife <= player.statLifeMax * .7f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax * .8f)
            {
                return .16f;
            }
            if (player.statLife <= player.statLifeMax * .9f)
            {
                return .8f;
            }
            return 0f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            string BoostType;
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (modPlayer.RangedHighest(player))
            {
                BoostType = "R";
            }
            else if (modPlayer.MagicHighest(player))
            {
                BoostType = "M";
            }
            else if (modPlayer.SummonHighest(player))
            {
                BoostType = "S";
            }
            else if (modPlayer.ThrownHighest(player))
            {
                BoostType = "T";
            }
            else
            {
                BoostType = "";
            }
            Texture2D texture = mod.GetTexture("Items/Boss/Rajah/RajahSash" + BoostType);
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                lightColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            string BoostType;
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (modPlayer.RangedHighest(player))
            {
                BoostType = "R";
            }
            else if (modPlayer.MagicHighest(player))
            {
                BoostType = "M";
            }
            else if (modPlayer.SummonHighest(player))
            {
                BoostType = "S";
            }
            else if (modPlayer.ThrownHighest(player))
            {
                BoostType = "T";
            }
            else
            {
                BoostType = "";
            }
            Texture2D texture = mod.GetTexture("Items/Boss/Rajah/RajahSash" + BoostType);
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}