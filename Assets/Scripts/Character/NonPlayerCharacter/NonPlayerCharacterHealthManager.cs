
namespace Scripts.Character.NonPlayerCharacter
{
    public class NonPlayerCharacterHealthManager : BaseHealthManager
    {
        //TODO: Add NonPlayerCharacterUIText and implement it in the same way as PlayerUIHealthText in PlayerHealthManager. This will allow for a health bar to be displayed above the NPC when they are damaged, similar to how it is done in many games.
        //private NonPlayerCharacterUIText NonPlayerCharacterUIText;

        
        protected override void Start()
        {
            base.Start();
        }

        private void ShowCharacterHealthUI()
        {

        }

        private void HideCharacterHealthUI()
        {
               
        }

        protected override void CheckAndUpdateHealthUI()
        {

        }

        protected override void TriggerDeath()
        {            
            IsDead = true;
            //Death animation?

            Destroy(gameObject);

        }
        
    }
}