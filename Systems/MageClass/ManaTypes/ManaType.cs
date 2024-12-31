using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Structs;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Systems.MageClass.ManaTypes
{
    public abstract class ManaType : ModType
    {
        /// <summary>
        /// How many extra segments are on the bar (looping texture, add extra segments right next to the existing segment texture)
        /// </summary>
        public virtual int loopingBarTextureSegments => 0;

        public bool consumable = true;
        public virtual string TexurePath => (base.GetType().Namespace + "." + this.Name).Replace('.', '/');

        /// <summary>
        /// Effect that executes when its mana is being used
        /// </summary>
        public virtual void UseEffect(Player player, NumberRange range)
        {

        }

        /// <summary>
        /// Effect that executes every tick when mana is not being used
        /// </summary>
        public virtual void NotInUseEffect(Player player, NumberRange range)
        {

        }

        /// <summary>
        /// Effect that executes every tick
        /// </summary>
        public virtual void TickEffect(Player player, NumberRange range)
        {

        }

        protected override void Register()
        {
            ModTypeLookup<ManaType>.Register(this);
        }
    }
}
