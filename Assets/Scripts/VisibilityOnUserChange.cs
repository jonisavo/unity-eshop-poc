using EshopPOC.API;

namespace EshopPOC
{
    public class VisibilityOnUserChange : VisibilityChange
    {
        private void OnEnable() => UserManager.OnUserChange += HandleUserChange;

        private void OnDisable() => UserManager.OnUserChange -= HandleUserChange;
        
        private void HandleUserChange(User user)
        {
            StopAllCoroutines();

            if (user == null)
                HandleNoUser();
            else
                HandleUser();
        }

        private void HandleNoUser()
        {
            StartCoroutine(action == Action.FadeIn ? HideCoroutine() : ShowCoroutine());
        }

        private void HandleUser()
        {
            StartCoroutine(action == Action.FadeIn ? ShowCoroutine() : HideCoroutine());
        }
    }
}