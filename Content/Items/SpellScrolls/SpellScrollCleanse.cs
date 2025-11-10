using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Templates;
using Terrafirma.Content.Skills.Cleanse;

namespace Terrafirma.Content.Items.SpellScrolls
{
    public class SpellScrollCleanse : SkillScroll
    {
        public override Skill SkillToLearn => new Cleanse();
    }
}
