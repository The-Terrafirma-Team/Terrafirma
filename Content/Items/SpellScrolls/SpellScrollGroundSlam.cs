using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Templates;
using Terrafirma.Content.Skills.GroundSlam;

namespace Terrafirma.Content.Items.SpellScrolls
{
    public class SpellScrollGroundSlam : SkillScroll
    {
        public override Skill SkillToLearn => new GroundSlam();
    }
}
