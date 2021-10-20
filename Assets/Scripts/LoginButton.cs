using UnityEngine;
using UnityEngine.UI;

namespace EshopPOC
{
    public class LoginButton : MonoBehaviour
    {
        public InputField emailInput;

        public InputField passwordInput;
        
        public enum Mode
        {
            Login,
            Register
        }

        public Mode mode;

        private UserManager _userManager;

        private void Start() =>
            _userManager = GameObject.Find("User Manager").GetComponent<UserManager>();

        public void Press()
        {
            if (mode == Mode.Login)
                _userManager.Login(emailInput.text, passwordInput.text);
            else
                _userManager.Register(emailInput.text, passwordInput.text);
        }
    }
}