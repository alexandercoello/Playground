
namespace Scripts.Character.NonPlayerCharacter
{
    public class NonPlayerCharacterStaminaManager : BaseStaminaManager
    {
       
        protected override void Start()
        {
            base.Start();
        }

        protected override void CheckAndUpdateStaminaUI()
        {
            //Nothing to do here that I can think of right now, since NPCs won't have a stamina bar displayed above them like the player does. If I decide to add that feature later, I can implement it here.
            return;
        }

    }
}