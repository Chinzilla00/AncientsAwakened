using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class RajahCape : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Cloak of Supremecy");
            Tooltip.SetDefault(@"Killed Rabbits revive as Ravaging Rabbits and maul your opponents
The Cotton Cane will summon Rajah Rabbit as its first summon as an ally
Only one Rajah may be active at one time. This includes other players.
Every 10% of health lost gives you 12% extra attack power to your highest damage type boost
Increased Jump Height and Speed
Grants Autojump
Immunity to fall damage
'You have been deemed a worthy opponent by the Pouncing Punisher'");
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 2;
            item.accessory = true;
            item.expert = true;
            item.defense = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(107, 137, 179);
                }
            }
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            Color damageColor = Color.Firebrick;
            string DamageType = "";
            if (modPlayer.MeleeHighest(player))
            {
                DamageType = "Melee";
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = "Ranged";
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = "Magic";
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = "Summoning";
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = "Throwing";
                damageColor = Color.DarkOrange;
            }

            string DamageAmmount = (10 * DamageBoost(player)) + "% ";

            TooltipLine DamageToltip = new TooltipLine(mod, "Damage Type", "Current Damage Boost: +" + DamageAmmount + DamageType + " Damage")
            {
                overrideColor = damageColor
            };

            tooltips.Add(DamageToltip);
            base.ModifyTooltips(tooltips);
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
                return 1.08f;
            }
            if (player.statLife <= player.statLifeMax * .2f)
            {
                return .96f;
            }
            if (player.statLife <= player.statLifeMax * .3f)
            {
                return .84f;
            }
            if (player.statLife <= player.statLifeMax * .4f)
            {
                return .72f;
            }
            if (player.statLife <= player.statLifeMax * .5f)
            {
                return .60f;
            }
            if (player.statLife <= player.statLifeMax * .6f)
            {
                return .48f;
            }
            if (player.statLife <= player.statLifeMax * .7f)
            {
                return .36f;
            }
            if (player.statLife <= player.statLifeMax * .8f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax * .9f)
            {
                return .12f;
            }
            return 0f;
        }
    }
    
}