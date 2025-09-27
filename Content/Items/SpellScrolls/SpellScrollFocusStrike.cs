using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Templates;
using Terrafirma.Content.Skills;

namespace Terrafirma.Content.Items.SpellScrolls
{
    public class SpellScrollFocusStrike : SkillScroll
    {
        public override Skill SkillToLearn => new FocusStrike();
    }
}
