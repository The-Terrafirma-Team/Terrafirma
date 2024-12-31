using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Systems.MageClass.ManaTypes
{
    public abstract class ManaType : ModType
    {
        public virtual string TexurePath => (base.GetType().Namespace + "." + this.Name).Replace('.', '/');

        /// <summary>
        /// Effect that executes when its mana is being used
        /// </summary>
        public virtual void UseEffect(Player player)
        {

        }

        /// <summary>
        /// Effect that executes every tick when mana is not being used
        /// </summary>
        public virtual void NotInUseEffect(Player player)
        {

        }

        /// <summary>
        /// Effect that executes every tick
        /// </summary>
        public virtual void TickEffect(Player player)
        {

        }

        protected override void Register()
        {
            ModTypeLookup<ManaType>.Register(this);
        }
    }
}
